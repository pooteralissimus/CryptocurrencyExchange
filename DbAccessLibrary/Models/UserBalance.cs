using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLibrary.Models
{
    public class UserBalance
    {
        public int Id { get; set; } 
        public string UserId { get; set; }
        public decimal Usdt { get; set; }
        public decimal Bitcoin { get; set; }
        public decimal Etherium { get; set; }
        public decimal Luna { get; set; }
        public decimal Solana { get; set; }
    }
}
