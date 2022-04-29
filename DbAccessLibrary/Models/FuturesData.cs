using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLibrary.Models
{
    public class FuturesData
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CoinName { get; set; }
        public int Leverage { get; set; }
        public decimal OpenPrice { get; set; }
        public string LongShort { get; set; }
        public decimal Quantity { get; set; }

    }
}
