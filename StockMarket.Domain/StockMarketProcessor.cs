namespace StockMarket.Domain
{
    public class StockMarketProcessor
    {

        private long lastOrderId;

        public StockMarketProcessor(long lastOrderId = 0)
        {
            this.lastOrderId = lastOrderId;
        }

        public void EnqueueOrder(TradeSide side, decimal quantity, decimal price)
        {
            if (side == TradeSide.Buy)
            {
                lastOrderId++;
                Order order = new(lastOrderId, side, quantity, price);
            }

            throw new NotImplementedException();
        }
    }
}
