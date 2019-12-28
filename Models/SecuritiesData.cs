using Newtonsoft.Json;

namespace PortfolioCalculator.Models 
{
    public class SecuritiesData 
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("prices")]
        public double[] Prices { get; set; }
    }
}