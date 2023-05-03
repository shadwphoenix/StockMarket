namespace StockMarket.Domain.Comparer
{
    public abstract class BaseComparer : IComparer<Order>
    {
        public int Compare(Order? x, Order? y)
        {
            var result = SpecificComparer(x, y);
            if (result != 0) return result;

            if (x.Id > y.Id) return -1;
            if (x.Id < y.Id) return 1;

            return 0;
        }

        protected abstract int SpecificComparer(Order? x, Order? y);
    }
}
