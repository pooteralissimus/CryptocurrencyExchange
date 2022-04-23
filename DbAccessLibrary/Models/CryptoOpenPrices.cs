using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLibrary.Models
{
    //model that saves day open price of coin to calculate 24h changes 
    public class CryptoOpenPrices  
    {
        public int Id { get; set; }
        public string CoinName { get; set; }
        public decimal OpenPrice { get; set; }
    }
}
