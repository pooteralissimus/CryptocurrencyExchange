using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLibrary.Models
{
    public class AccountBalance
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CoinName { get; set; }
        public decimal Quantity { get; set; }
    }
}
