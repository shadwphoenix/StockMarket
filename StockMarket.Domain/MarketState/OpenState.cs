namespace StockMarket.Domain.MarketState
{
    internal class OpenState : MarketState
    {
        public OpenState(StockMarketProcessor stockMarketProcessor) : base(stockMarketProcessor)
        {
        }

        public override void CloseMarket()
        {
            stockMarketProcessor.Close();
        }
        public override void OpenMarket()
        {
        }
        public override long EnqueueOrder(TradeSide side, decimal quantity, decimal price)
        {
            return stockMarketProcessor.Enqueue(side, quantity, price);
        }
        public override long CancelOrder(long orderId)
        {
            return stockMarketProcessor.Cancel(orderId);
        }

        public override long ModifyOrder(long orderId, decimal quantity, decimal price)
        {
            return stockMarketProcessor.Modify(orderId,quantity,price);
        }
    }
}