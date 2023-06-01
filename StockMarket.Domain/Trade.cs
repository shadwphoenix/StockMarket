namespace StockMarket.Domain
{
    public class Trade
    {
        internal Trade(long tradeId, long idSell, long idBuy, decimal tradeQuantity, decimal price)
        {
            sellOrderId = idSell;
            buyOrderId = idBuy;
            Quantity = tradeQuantity;
            Price = price;
            Id = tradeId;
        }

        public long sellOrderId { get; }
        private long buyOrderId { get; }
        public decimal Quantity { get; }
        public decimal Price { get; }
        public long Id { get; }
    }
}