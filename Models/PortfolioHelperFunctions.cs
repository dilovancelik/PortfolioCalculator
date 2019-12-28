using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using MathNet.Numerics.LinearAlgebra;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace PortfolioRiskReturn.Models
{
    public class HelperFunctions
    {


        private Vector<double> GenerateDemeanedPriceVectors(double[] prices)
        {
            var prices_vector = Vector<double>.Build.DenseOfArray(prices);
            var average_price = prices_vector.Average();
            var df = (1 / (prices.Length - 1));
            var price_variance_vector = prices_vector.Subtract(average_price);
            return price_variance_vector;
        }

        private Matrix<double> GenerateCoverianceMatrix(List<double[]> list_of_prices)
        {
            var prices_vectors = new Vector<double>[list_of_prices.Count];
            list_of_prices.ForEach(
                prices =>
                prices_vectors[list_of_prices.IndexOf(prices)] = GenerateDemeanedPriceVectors(prices));

            var prices_matrix = Matrix<double>.Build.DenseOfColumnVectors(prices_vectors);
            var covariance_matrix = prices_matrix
                .TransposeThisAndMultiply(prices_matrix)
                .Divide(list_of_prices[0].Count() - 1);

            return covariance_matrix;
        }

        public Vector<double> GeneratePortfolioWeights(List<double> values)
        {
            var total_portfolio_value = values.Sum();
            var weights = new List<double> {};
            values.ForEach(
                value => 
                weights.Add(value / total_portfolio_value)
            );

            var weights_vector = Vector<double>.Build.DenseOfArray(weights.ToArray());
            return weights_vector;
        }

    }
}