using System;
using Moq;
using NUnit.Framework;

namespace MemoryCacheT.Test.CacheItem
{
    internal class CacheItemTestBase
    {
        protected ICacheItem<int> CacheItem;
        protected Mock<IDateTimeProvider> DateTimeProviderMock;
        protected int Value;
        protected DateTime Now;

        [SetUp]
        public void Setup()
        {
            Now = new DateTime(2012, 8, 20);
            DateTimeProviderMock = new Mock<IDateTimeProvider>(MockBehavior.Strict);
            Value = 7;
            CacheItem = CreateCacheItem();
        }

        protected virtual ICacheItem<int> CreateCacheItem()
        {
            return new NonExpiringCacheItem<int>(DateTimeProviderMock.Object, Value);
        }

        [TearDown]
        public void TearDown()
        {
            DateTimeProviderMock.VerifyAll();
            FinalizeTearDown();
        }

        protected virtual void FinalizeTearDown() { }

    }
}