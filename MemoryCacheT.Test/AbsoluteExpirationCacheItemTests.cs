using System;
using NUnit.Framework;

namespace MemoryCacheT.Test
{
    [TestFixture]
    internal class AbsoluteExpirationCacheItemTests : CacheItemTestBase
    {
        private DateTime _expirationDate;

        protected override ICacheItem<int> CreateCacheItem()
        {
            _expirationDate = Now.AddDays(1);
            return new AbsoluteExpirationCacheItem<int>(DateTimeProviderMock.Object, Value, _expirationDate);
        }

        [Test]
        public void IsExpired_CurentDateTimeIsLessThanExpirationDate_ReturnsFalse()
        {
            DateTimeProviderMock.SetupGet(item => item.Now).Returns(Now);
            bool isExpired = CacheItem.IsExpired();

            Assert.False(isExpired);
        }


        [Test]
        public void IsExpired_CurentDateTimeIsGreaterThanExpirationDate_ReturnsTrue()
        {
            Now = _expirationDate.AddDays(1);
            DateTimeProviderMock.SetupGet(item => item.Now).Returns(Now);
            bool isExpired = CacheItem.IsExpired();

            Assert.True(isExpired);
        }
    }
}