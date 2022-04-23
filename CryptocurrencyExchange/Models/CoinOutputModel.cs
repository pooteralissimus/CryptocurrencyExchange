namespace CryptocurrencyExchange.Models
{
    public class CoinOutputModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; } 
        public decimal DayOpenPrice { get; set; }
        public double changes24h { get; set; }

    }
}
