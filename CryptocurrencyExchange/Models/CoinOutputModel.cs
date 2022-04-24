using System;

namespace CryptocurrencyExchange.Models
{
    public class CoinOutputModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal DayOpenPrice { get; set; }

        private double _changes24h;
        public double changes24h
        {
            get { return _changes24h; }
            set
            {
                _changes24h = Math.Round(value, 2);
            }
        }

    }
}
