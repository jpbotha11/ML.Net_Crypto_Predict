using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;


namespace Price_Predict
{
    class Program
    {
        //globals
        static string file_path = @"C:\crypto_data\";
        static int days = 100;

        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(file_path, "*.csv");

            //first
            foreach (string file_coin in files)
            {
                //get only coin 
                string coin = Path.GetFileName(file_coin).Replace(".csv","");

                Predict_BTC("BTC", coin);
            }
            

            // Predict_Coin("ZRC");
        }

        static void Predict_BTC(string coin, string coin_to_predict)
        {
            //variables
            DateTime start_date = new DateTime();
            DateTime new_date = new DateTime();

            //instantinate
            MLContext mlContext = new MLContext();
            string path = file_path + coin + ".csv";

            //read into enum
            string[] data = File.ReadAllLines(path);

            //parse to ienum
            List<input> lst_data = new List<input>();
            int i = 0;
            int day_count = 0;

            //loop each line in csv
            foreach (string data_row in data)
            {
                //skip the header
                i++;
                if (i > 1)
                {
                    //set day
                    day_count++;

                    //split line to csv
                    string[] csv_data = data_row.Split(",");

                    //new class
                    input inpy = new input();
                    inpy.Close = float.Parse(csv_data[0]);
                    inpy.High = float.Parse(csv_data[1]);
                    inpy.Date = DateTime.Parse(csv_data[2]);
                    inpy.day_no = day_count;

                    //add to main list
                    lst_data.Add(inpy);
                }
            }

            //get first date
            start_date = lst_data[0].Date;

            //loop till done
            for (int l = 0; l < days; l++)            
            {
                //1
                IDataView baseTrainingDataView = mlContext.Data.LoadFromEnumerable<input>(lst_data);


                //2 pipeline
                var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "high")     //outputColumnName is altyd Label -   inputColumnName is die naam van die value wat jy wil predict                           
                                                                                                                        //.Append(mlContext.Transforms.Categorical.OneHotEncoding("season_e","season"))
                       .Append(mlContext.Transforms.Concatenate("Features", "day_no")//die values wat die inputColumnName sal bepaal
                       .Append(mlContext.Regression.Trainers.FastForest()));//die algoritme

                //3 test
                var test = mlContext.Data.TrainTestSplit(baseTrainingDataView, testFraction: 0.3);

                //4 train
                var trainedModel = pipeline.Fit(baseTrainingDataView);

                //5 eval
                IDataView predictions = trainedModel.Transform(test.TestSet);
                var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");

                //6 predict
                var predictionFunction = mlContext.Model.CreatePredictionEngine<input, Predict>(trainedModel);

                //do the prediction
                day_count++;
                var prediction_loop = predictionFunction.Predict(new input() { day_no = day_count });

                //now add the prediction to main list and train again
                input new_input = new input();
                new_input.day_no = day_count;
                new_input.High = prediction_loop.price;

                //add to list
                lst_data.Add(new_input);

                //display
                Console.WriteLine("Day: " + day_count + "BTC - Price: " + prediction_loop.price);

                //now predict other coin based on BTC price
                Predict_Coin(coin_to_predict, prediction_loop.price, day_count);
            }
        }

        static void Predict_Coin(string coin, float btc_high, int day)
        {
            //variables
            DateTime start_date = new DateTime();
            DateTime new_date = new DateTime();

            //instantinate
            MLContext mlContext = new MLContext();
            string path = file_path +  coin + ".csv";

            //read into enum
            string[] data = File.ReadAllLines(path);

            //parse to ienum
            List<input> lst_data = new List<input>();
            int i = 0;
            int day_count = 0;

            //loop each line in csv
            foreach (string data_row in data)
            {
                //skip the header
                i++;
                if (i > 1)
                {
                    //set day
                    day_count++;

                    //split line to csv
                    string[] csv_data = data_row.Split(",");

                    //new class
                    input inpy = new input();
                    inpy.Close = float.Parse(csv_data[0]);
                    inpy.High = float.Parse(csv_data[1]);
                    inpy.Date = DateTime.Parse(csv_data[2]);
                    inpy.btc_price = float.Parse(csv_data[8]);
                    inpy.day_no = day_count;

                    //add to main list
                    lst_data.Add(inpy);
                }
            }

            //get first date
            start_date = lst_data[0].Date;

            //work out end date
            DateTime end_date = start_date.AddDays(day_count);

            //loop till done
            // while (true)
            //{
            //1
            IDataView baseTrainingDataView = mlContext.Data.LoadFromEnumerable<input>(lst_data);


            //2 pipeline
            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "high")     //outputColumnName is altyd Label -   inputColumnName is die naam van die value wat jy wil predict                           
                                                                                                                    //.Append(mlContext.Transforms.Categorical.OneHotEncoding("season_e","season"))
                   .Append(mlContext.Transforms.Concatenate("Features", "day_no", "btc_price")//die values wat die inputColumnName sal bepaal
                   .Append(mlContext.Regression.Trainers.FastForest()));//die algoritme

            //3 test
            var test = mlContext.Data.TrainTestSplit(baseTrainingDataView, testFraction: 0.3);

            //4 train
            var trainedModel = pipeline.Fit(baseTrainingDataView);

            //5 eval
            IDataView predictions = trainedModel.Transform(test.TestSet);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");

            //6 predict
            var predictionFunction = mlContext.Model.CreatePredictionEngine<input, Predict>(trainedModel);

            //do the prediction            
            var prediction_loop = predictionFunction.Predict(new input() { day_no = day_count, btc_price = btc_high });

            //now add the prediction to main list and train again
            input new_input = new input();
            new_input.day_no = day_count;
            new_input.High = prediction_loop.price;

            //add to list
            lst_data.Add(new_input);

            //display
            Console.WriteLine("Day: " + day_count + " - Price: " + prediction_loop.price);

            //add and save to csv
            string csv_header = "close,high,date,low,open,volume_from,volume_to,symbol,btc_price";
            string csv_record = "0," + prediction_loop.price + "," + end_date.ToString("yyyy-MM-dd") + ",0,0,0,0," + coin + "," + btc_high + Environment.NewLine; 
            System.IO.File.AppendAllText(path, csv_record);
            //}
        }
    }

    public class input
    {
        [ColumnName("close"), LoadColumn(0)]
        public float Close { get; set; }


        [ColumnName("high"), LoadColumn(1)]
        public float High { get; set; }


        [ColumnName("date"), LoadColumn(2)]
        public DateTime Date { get; set; }


        [ColumnName("low"), LoadColumn(3)]
        public float Low { get; set; }


        [ColumnName("open"), LoadColumn(4)]
        public float Open { get; set; }


        [ColumnName("volume_from"), LoadColumn(5)]
        public float Volume_from { get; set; }


        [ColumnName("volume_to"), LoadColumn(6)]
        public float Volume_to { get; set; }


        [ColumnName("symbol"), LoadColumn(7)]
        public string Symbol { get; set; }

        [ColumnName("btc_price"), LoadColumn(8)]
        public float btc_price { get; set; }

        [ColumnName("day_no"), LoadColumn(9)]
        public float day_no { get; set; }



    }

    class Predict
    {
        [ColumnName("Score")]
        public float price { get; set; }
    }
}
