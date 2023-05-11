namespace StockMarket.Domain
{
    public interface IStockMarketProcessor
    {
        IEnumerable<Order> Orders { get; }
        IEnumerable<Trade> Trades { get; }

        long CancelOrder(long orderId);
        void CloseMarket();
        void OpenMarket();
        long EnqueueOrder(TradeSide side, decimal quantity, decimal price);
    }
}