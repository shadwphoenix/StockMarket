namespace StockMarket.Domain.MarketState
{
    internal abstract class MarketState : IStockMarketProcessor
    {
        protected StockMarketProcessor stockMarketProcessor;

        protected MarketState(StockMarketProcessor stockMarketProcessor)
        {
            this.stockMarketProcessor = stockMarketProcessor;
        }

        public IEnumerable<Order> Orders => throw new NotImplementedException();

        public IEnumerable<Trade> Trades => throw new NotImplementedException();

        public virtual long CancelOrder(long orderId)
        {
            throw new NotImplementedException();
        }

        public virtual void CloseMarket()
        {
            throw new NotImplementedException();
        }

        public virtual long EnqueueOrder(TradeSide side, decimal quantity, decimal price)
        {
            throw new NotImplementedException();
        }

        public virtual void OpenMarket()
        {
            throw new NotImplementedException();
        }
    }
}