using System;
using Moq;
using NUnit.Framework;

namespace MemoryCacheT.Test.CacheItem
{
    internal class CacheItemTestBase
    {
        protected ICacheItem<int> _cacheItem;
        protected Mock<IDateTimeProvider> _dateTimeProviderMock;
        protected int _value;
        protected DateTime _now;

        [SetUp]
        public void Setup()
        {
            _now = new DateTime(2012, 8, 20);
            _dateTimeProviderMock = new Mock<IDateTimeProvider>(MockBehavior.Strict);
            _value = 7;
            _cacheItem = CreateCacheItem();
        }

        protected virtual ICacheItem<int> CreateCacheItem()
        {
            return new NonExpiringCacheItem<int>(_dateTimeProviderMock.Object, _value);
        }

        [TearDown]
        public void TearDown()
        {
            _dateTimeProviderMock.VerifyAll();
            FinalizeTearDown();
        }

        protected virtual void FinalizeTearDown() { }

    }
}