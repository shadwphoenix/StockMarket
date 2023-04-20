namespace StockMarket.Domain
{
    public class StockMarketProcessor
    {

        long lastOrderId;

        public StockMarketProcessor()
        {
            lastOrderId = 0;
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
