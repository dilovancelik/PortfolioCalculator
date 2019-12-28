using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using MathNet.Numerics.LinearAlgebra;
using PortfolioCalculator.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace PortfolioRiskReturn.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReturnsController : Controller
    {

        [HttpPost]
        public ActionResult<string> Post(List<SecuritiesData> portfolio)
        {
            var returns = new List<double> {};
            var values = new List<double> {};

            foreach (var security in portfolio) 
            {
                returns.Add((security.Prices.Last() / security.Prices.First()) - 1);
                values.Add(security.Amount * security.Prices.Last());
            }
            var returns_vector = Vector<double>.Build.DenseOfArray(returns.ToArray());
            var weights_vector = GeneratePortfolioWeights(values);

            var portfolio_returns = weights_vector
                .ToRowMatrix()
                .Multiply(returns_vector);
                
            return portfolio_returns.ToString();
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