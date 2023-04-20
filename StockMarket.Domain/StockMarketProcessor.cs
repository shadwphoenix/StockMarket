namespace StockMarket.Domain
{
    public class StockMarketProcessor
    {

        private long lastOrderId;
        private readonly PriorityQueue<Order,Order> buyOrders;
        private readonly PriorityQueue<Order,Order> sellOrders;
        public StockMarketProcessor(long lastOrderId = 0)
        {
            this.lastOrderId = lastOrderId;
            buyOrders = new PriorityQueue<Order,Order>();
            sellOrders = new PriorityQueue<Order,Order>();

        }

        public void EnqueueOrder(TradeSide side, decimal quantity, decimal price)
        {
            if (side == TradeSide.Buy)
            {
                Interlocked.Increment(ref lastOrderId);
                Order order = new(lastOrderId, side, quantity, price);
                ProcessBuyOrder(order);
            }
            else
            {
                Interlocked.Increment(ref lastOrderId);
                Order order = new(lastOrderId, side, quantity, price);
                ProcessSellOrder(order);
            }
            throw new NotImplementedException();
        }

        private void ProcessSellOrder(Order order)
        {
            throw new NotImplementedException();
        }

        private void ProcessBuyOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
