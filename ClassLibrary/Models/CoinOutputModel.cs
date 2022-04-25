using System;

namespace CryptocurrencyExchange.Models
{
    public class CoinOutputModel
    {
        public string Name { get; set; }
        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                if (value < 10)
                    price = Math.Round(value, 4);
                else price = Math.Round(value, 2);
            }
        }
        public decimal DayOpenPrice { get; set; }

        private double changes24h;
        public double Changes24h
        {
            get { return changes24h; }
            set
            {
                changes24h = Math.Round(value, 2);
            }
        }
        private int floatRound { get; set; }

    }
}
