using DbAccessLibrary.DataAccess;
using System.Collections.Generic;

namespace ClassLibrary
{
    public static class CryptocurrencyOperations
    {

        public static List<dynamic> GetPrices(string[] coinsName)
        {
            List<dynamic> coinsPrice = new List<dynamic>();
            string json;

            foreach (var coinName in coinsName)
            {
                using (var web = new System.Net.WebClient())
                {
                    var url = $"https://api.binance.com/api/v3/ticker/price?symbol={coinName}";
                    json = web.DownloadString(url);
                }

                dynamic coinInfo = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                coinsPrice.Add(coinInfo);

            }
            return coinsPrice;

        }

        public static double Get24hChanges(string coinName,MyDbContext ctx)
        {
            double result = 0;

            return result;
        }
    }

}
