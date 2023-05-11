namespace StockMarket.Domain.MarketState
{
    internal class CloseState : MarketState
    {
        public CloseState(StockMarketProcessor stockMarketProcessor) : base(stockMarketProcessor)
        {
        }

        public override void CloseMarket()
        {
        }

        public override void OpenMarket()
        {
            stockMarketProcessor.Open();
        }

        public override long EnqueueOrder(TradeSide side, decimal quantity, decimal price)
        {
            return base.EnqueueOrder(side, quantity, price);
        }

        public override long CancelOrder(long orderId)
        {
            return base.CancelOrder(orderId);
        }
    }
}