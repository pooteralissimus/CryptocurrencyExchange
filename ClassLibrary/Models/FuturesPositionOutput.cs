using System;

namespace ClassLibrary.Models
{
    public class FuturesPositionOutput
    {
        public string CoinName { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        private decimal startedTotal;
        public decimal StartedTotal { get { return startedTotal; } set { startedTotal =Math.Round(value,2); } }
        private decimal currentTotal;
        public decimal CurrentTotal { get { return currentTotal; } set { currentTotal = Math.Round(value, 2); } }
        public decimal UsdtDifferece
        {
            get
            {
                return Math.Round(currentTotal - startedTotal, 2);
            }
        }
        public int Leverage { get; set; }
        public float PercentChanges { get; set; }

    }
}
