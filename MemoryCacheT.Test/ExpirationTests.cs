using System;
using System.Timers;
using Moq;
using NUnit.Framework;

namespace MemoryCacheT.Test
{
    [TestFixture]
    internal class ExpirationTests : CacheTestBase
    {
        private Mock<ICacheItem<int>> _cacheItemMock1;
        private Mock<ICacheItem<int>> _cacheItemMock2;
        private Mock<ICacheItem<int>> _cacheItemMock3;

        private string _key1;
        private string _key2;
        private string _key3;

        protected override void FinalizeSetup()
        {
            _cacheItemMock1 = new Mock<ICacheItem<int>>(MockBehavior.Strict);
            _cacheItemMock2 = new Mock<ICacheItem<int>>(MockBehavior.Strict);
            _cacheItemMock3 = new Mock<ICacheItem<int>>(MockBehavior.Strict);

            _key1 = "key1";
            _key2 = "key2";
            _key3 = "key3";
        }

        protected override void FinalizeTearDown()
        {
            _cacheItemMock1.VerifyAll();
            _cacheItemMock2.VerifyAll();
            _cacheItemMock3.VerifyAll();
        }

        [Test]
        public void Expiration_TwoItemsExpire_ExpireCallbacksAreInvoked()
        {
            _cacheItemMock1.Setup(item => item.IsExpired).Returns(true);
            _cacheItemMock2.Setup(item => item.IsExpired).Returns(true);
            _cacheItemMock3.Setup(item => item.IsExpired).Returns(false);

            _cacheItemMock1.Setup(item => item.Expire()).Verifiable();
            _cacheItemMock2.Setup(item => item.Expire()).Verifiable();

            AddItems();

            ElapseTimer();
        }

        [Test]
        public void Expiration_OneItemExpires_ItemIsRemoved()
        {
            _cacheItemMock1.Setup(item => item.IsExpired).Returns(true);
            _cacheItemMock2.Setup(item => item.IsExpired).Returns(false);
            _cacheItemMock3.Setup(item => item.IsExpired).Returns(false);

            _cacheItemMock1.Setup(item => item.Expire()).Verifiable();

            AddItems();
            ElapseTimer();

            Assert.False(_cache.ContainsKey(_key1));
        }

        private void AddItems()
        {
            _cache.Add(_key1, _cacheItemMock1.Object);
            _cache.Add(_key2, _cacheItemMock2.Object);
            _cache.Add(_key3, _cacheItemMock3.Object);
        }

        private void ElapseTimer()
        {
            _timerMock.Setup(item => item.Stop()).Verifiable();
            _timerMock.Setup(item => item.Start()).Verifiable(); 
            _timerMock.Raise(item => item.Elapsed += null, new EventArgs() as ElapsedEventArgs);
        }
    }
}