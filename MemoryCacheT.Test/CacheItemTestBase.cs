using System;
using NUnit.Framework;

namespace MemoryCacheT.Test
{
    public class CacheItemTestBase
    {
        protected ICacheItem<int> CacheItem;
        protected int Value;
        protected DateTime Now;

        [SetUp]
        public void Setup()
        {
            Now = new DateTime(2012, 8, 20);
            Value = 7;
            CacheItem = CreateCacheItem();
        }

        protected virtual ICacheItem<int> CreateCacheItem()
        {
            return new NonExpiringCacheItem<int>(Value);
        }
    }
}