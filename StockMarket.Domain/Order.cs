namespace StockMarket.Domain
{
    public class Order
    {
        internal Order(long id,TradeSide side, decimal quantity, decimal price)
        {
            Id = id;
            Side = side;
            Quantity = quantity;
            Price = price;
        }
        
        public long Id { get; }
        public TradeSide Side { get; }
        public decimal Quantity { get; }
        public decimal Price { get; }
    }
}