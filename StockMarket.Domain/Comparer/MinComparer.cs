using StockMarket.Domain;
using StockMarket.Domain.Comparer;

internal class MinComparer : BaseComparer
{
    protected override int SpecificComparer(Order? x, Order? y)
    {
        if (x.Price > y.Price) return 1;
        if (y.Price > x.Price) return -1;
        return 0;
    }
}
