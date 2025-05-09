using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using Model;

namespace MachineLearning
{
    public class LendingPrediction
    {

        public class LendingData
        {
            public float MonthIndex { get; set; }
            public float NumberOfLendings { get; set; }
        }

        private class LendingPredictionResult
        {
            [ColumnName("Score")]
            public float NumberOfLendings { get; set; }
        }

        public static float PredictForThisMonth(Book book, IEnumerable<Lending> allLendings)
        {
            var mlContext = new MLContext();

            var bookLendings = allLendings
                .Where(l => l.BookId == book.Id)
                .GroupBy(l => new { l.LendingPeriodStart.Year, l.LendingPeriodStart.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .Select((g, index) => new LendingData
                {
                    MonthIndex = index,
                    NumberOfLendings = g.Count()
                })
                .ToList();

            if (bookLendings.Count < 3)
                return 0;

            
            var data = mlContext.Data.LoadFromEnumerable(bookLendings);

            var pipeline = mlContext.Transforms
                .Concatenate("Features", nameof(LendingData.MonthIndex))
                .Append(mlContext.Regression.Trainers.FastTree(
                    labelColumnName: nameof(LendingData.NumberOfLendings),
                    featureColumnName: "Features"));

            var model = pipeline.Fit(data);

            
            int nextMonthIndex = bookLendings.Count-1; 
            var predictionEngine = mlContext.Model.CreatePredictionEngine<LendingData, LendingPredictionResult>(model);

            var prediction = predictionEngine.Predict(new LendingData { MonthIndex = nextMonthIndex });

            return Math.Max(0, prediction.NumberOfLendings); 
        }


    }
}
