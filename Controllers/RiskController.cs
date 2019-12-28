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
    public class RiskController : Controller
    {

        [HttpPost]
        public ActionResult<string> Post(List<SecuritiesData> portfolio)
        {
            var helper_functions = new Models.HelperFunctions();
            var list_of_prices = new List<double[]> {};
            var values = new List<double> {};

            foreach (var security in portfolio) 
            {
                list_of_prices.Add(security.Prices);
                values.Add(security.Amount * security.Prices.Last());
            }


            var weights_vector = helper_functions.GeneratePortfolioWeights(values);
            var covariance_matrix = helper_functions.GenerateCoverianceMatrix(list_of_prices);

            var portfolio_variance = weights_vector
                .ToRowMatrix()
                .Multiply(covariance_matrix)
                .Multiply(weights_vector)
                .First();
                
            var portfolio_std_dev = Math.Sqrt(portfolio_variance);

            return portfolio_std_dev.ToString();
        }

    }
}