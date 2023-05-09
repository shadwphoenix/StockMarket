using System.Numerics;

namespace StockMarket.Domain
{
    public class Order
    {
        internal Order(long id, TradeSide side, decimal quantity, decimal price)
        {
            Id = id;
            Side = side;
            Quantity = quantity;
            Price = price;
            IsCanceled = false;
        }

        public long Id { get; }
        public TradeSide Side { get; }
        public decimal Quantity { get; private set; }
        public decimal Price { get; }
        public bool IsCanceled { get; private set; }

        internal void CancelOrder()
        {
            IsCanceled = true;
        }

        internal void DecreaseQuantity(decimal quantity)
        {
            Quantity -= quantity;
        }
    }
}