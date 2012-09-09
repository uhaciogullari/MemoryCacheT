using NUnit.Framework;

namespace MemoryCacheT.Test.CollectionOperations
{
    [TestFixture]
    internal class ClearTests : CacheTestBase
    {
        [Test]
        public void Clear_CacheIsNotEmpty_AllItemsAreRemoved()
        {
            _cache.Add("1", new NonExpiringCacheItem<int>(1));
            _cache.Add("2", new NonExpiringCacheItem<int>(2));
            _cache.Add("3", new NonExpiringCacheItem<int>(3));

            _cache.Clear();

            Assert.AreEqual(default(int), _cache.Count);
        }

        [Test]
        public void Clear_CacheIsNotEmpty_OnRemoveIsCalled()
        {
            bool onRemoveIsCalled = false;
            _cacheItem.OnRemove = (i, time) => onRemoveIsCalled = true;
            _cache.Add(_key, _cacheItem);

            _cache.Clear();

            Assert.True(onRemoveIsCalled);
        }
    }
}