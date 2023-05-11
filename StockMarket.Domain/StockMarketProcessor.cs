namespace StockMarket.Domain
{
    public class StockMarketProcessor
    {

        private long lastOrderId;
        private long lastTradeId;
        private readonly PriorityQueue<Order, Order> buyOrders;
        private readonly PriorityQueue<Order, Order> sellOrders;
        private readonly List<Trade> trades;
        public IEnumerable<Trade> Trades => trades;

        private readonly List<Order> orders;
        public IEnumerable<Order> Orders => orders;

        public StockMarketProcessor(long lastOrderId = 0, long lastTradeId = 0)
        {
            this.lastOrderId = lastOrderId;
            this.lastTradeId = lastTradeId;
            buyOrders = new(new MaxComparer());
            sellOrders = new(new MinComparer());
            trades = new();
            orders = new();
        }

        public long EnqueueOrder(TradeSide side, decimal quantity, decimal price)
        {
            Interlocked.Increment(ref lastOrderId);
            Order order = new(lastOrderId, side, quantity, price);
            if (side == TradeSide.Buy)
            {
                matchOrder(sellOrders, buyOrders, order, (price1, price2) => price1 <= price2);

            }
            else
            {
                matchOrder(buyOrders, sellOrders, order, (price1, price2) => price1 >= price2);
            }
            orders.Add(order);
            return order.Id;
        }
        private void makeTrade(Order order1, Order order2)
        {
            var minQuantity = Math.Min(order1.Quantity, order2.Quantity);
            order1.DecreaseQuantity(minQuantity);
            order2.DecreaseQuantity(minQuantity);

            Interlocked.Increment(ref lastTradeId);

            var matchingOrders = findOrders(order1, order2);

            Trade trade = new Trade(lastTradeId, matchingOrders.SellOrder.Id, matchingOrders.BuyOrder.Id, minQuantity, matchingOrders.SellOrder.Price);

            trades.Add(trade);
        }

        private void matchOrder(PriorityQueue<Order, Order> matchingOrders, PriorityQueue<Order, Order> Orders, Order order, Func<decimal, decimal, bool> comparePriceDelegate)
        {
            while (matchingOrders.Count > 0 && comparePriceDelegate(matchingOrders.Peek().Price, order.Price) && order.Quantity > 0)
            {
                Order peekTargetOrder = matchingOrders.Peek();

                if (peekTargetOrder.IsCanceled)
                {
                    matchingOrders.Dequeue();
                    continue;
                }

                makeTrade(order, peekTargetOrder);

                if (peekTargetOrder.Quantity == 0) matchingOrders.Dequeue();
            }
            if (order.Quantity > 0) Orders.Enqueue(order, order);
        }
        private static (Order SellOrder, Order BuyOrder) findOrders(Order order1, Order order2)
        {
            if (order1.Side == TradeSide.Buy)
            {
                return (order2, order1);
            }
            return (order1, order2);
        }

        public void CancelOrder(long orderId)
        {
            var order = orders.Single(o => o.Id == orderId);
            order.CancelOrder();
        }

    }
}
