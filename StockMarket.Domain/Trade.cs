namespace StockMarket.Domain
{
    public class Trade
    {
        internal Trade(long idSell, long idBuy, decimal tradeQuantity, decimal price)
        {
            IdSell = idSell;
            IdBuy = idBuy;
            TradeQuantity = tradeQuantity;
            Price = price;
        }

        public long IdSell { get; }
        public long IdBuy { get; }
        public decimal TradeQuantity { get; }
        public decimal Price { get; }
    }
}