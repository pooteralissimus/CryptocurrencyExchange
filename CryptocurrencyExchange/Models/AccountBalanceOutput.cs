namespace CryptocurrencyExchange.Models
{
    public class AccountBalanceOutput
    {
        public string CoinName { get; set; }
        public decimal CoinPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal UsdtConvert
        {
            get
            {
                return CoinPrice * Quantity;
            }
        }
    }
}
