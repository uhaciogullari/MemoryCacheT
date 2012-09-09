namespace MemoryCacheT
{
    public interface ICacheItemFactory
    {
        ICacheItem<TValue> CreateInstance<TValue>(TValue value);
    }
}