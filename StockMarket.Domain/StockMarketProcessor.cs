namespace StockMarket.Domain
{
    public class StockMarketProcessor
    {

        private long lastOrderId;
        private readonly PriorityQueue<Order, Order> buyOrders;
        private readonly PriorityQueue<Order, Order> sellOrders;
        private readonly List<Trade> trades;
        public IEnumerable<Trade> Trades => trades;

        private readonly List<Order> orders;
        public IEnumerable<Order> Orders => orders;

        public StockMarketProcessor(long lastOrderId = 0)
        {
            this.lastOrderId = lastOrderId;
            buyOrders = new PriorityQueue<Order, Order>(new MaxComparer());
            sellOrders = new PriorityQueue<Order, Order>(new MinComparer());
            trades = new List<Trade>();
            orders = new List<Order>();

        }

        public void EnqueueOrder(TradeSide side, decimal quantity, decimal price)
        {
            Interlocked.Increment(ref lastOrderId);
            Order order = new(lastOrderId, side, quantity, price);
            orders.Add(order);
            if (side == TradeSide.Buy)
            {
                ProcessBuyOrder(order);
            }
            else
            {
                ProcessSellOrder(order);
            }
        }

        private void ProcessSellOrder(Order order)
        {


            while (buyOrders.Count > 0 && buyOrders.Peek().Price > order.Price && order.Quantity > 0)
            {
                Order peekBuyOrder = buyOrders.Peek();
                var tradeQuantity = Math.Min(peekBuyOrder.Quantity, order.Quantity);
                peekBuyOrder.DecreaseQuantity(tradeQuantity);
                order.DecreaseQuantity(tradeQuantity);
                Trade trade = new Trade(order.Id, peekBuyOrder.Id, tradeQuantity, order.Price);
                trades.Add(trade);
                if (peekBuyOrder.Quantity == 0)
                {
                    buyOrders.Dequeue();
                }
            }

            if (order.Quantity > 0)
            {
                sellOrders.Enqueue(order, order);
            }
        }

        private void ProcessBuyOrder(Order order)
        {

            while (sellOrders.Count > 0 && sellOrders.Peek().Price < order.Price && order.Quantity > 0)
            {
                Order peekSellOrder = sellOrders.Peek();
                var tradeQuantity = Math.Min(order.Quantity, order.Quantity);
                peekSellOrder.DecreaseQuantity(tradeQuantity);
                order.DecreaseQuantity(tradeQuantity);
                Trade trade = new Trade(peekSellOrder.Id, order.Id, tradeQuantity, order.Price);
                trades.Add(trade);
                if (peekSellOrder.Quantity == 0)
                {
                    sellOrders.Dequeue();
                }
            }
            if (order.Quantity > 0)
            {
                buyOrders.Enqueue(order, order);
            }
        }

        private void MakeTrade()
    }
}
