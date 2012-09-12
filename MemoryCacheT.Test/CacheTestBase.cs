using System;
using Moq;
using NUnit.Framework;

namespace MemoryCacheT.Test
{
    internal class CacheTestBase
    {
        protected ICache<string, int> _cache;
        protected Mock<ITimer> _timerMock;
        protected Mock<ICacheItemFactory> _cacheItemFactoryMock;
        protected TimeSpan _timerInterval;
        protected int _value;
        protected string _key;
        protected ICacheItem<int> _cacheItem;

        [SetUp]
        public void Setup()
        {
            _timerMock = new Mock<ITimer>(MockBehavior.Strict);
            _timerInterval = new TimeSpan(0, 0, 1, 0);

            _timerMock.SetupSet(mock => mock.Interval = It.Is<double>(interval => interval > double.Epsilon));
            _timerMock.Setup(mock => mock.Start()).Verifiable();

            _cacheItemFactoryMock = new Mock<ICacheItemFactory>(MockBehavior.Strict);

            _value = 7;
            _key = "key";
            _cacheItem = new NonExpiringCacheItem<int>(_value);

            _cache = new Cache<string, int>(_timerMock.Object, _timerInterval, _cacheItemFactoryMock.Object);
            FinalizeSetup();
        }

        protected virtual void FinalizeSetup() { }

        [TearDown]
        public void TearDown()
        {
            _timerMock.VerifyAll();
            _cacheItemFactoryMock.VerifyAll();
            FinalizeTearDown();
        }

        protected virtual void FinalizeTearDown() { }
    }
}