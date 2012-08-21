using System;
using NUnit.Framework;

namespace MemoryCacheT.Test
{
    [TestFixture]
    public class AbsoluteExpirationCacheItemTests : CacheItemTestBase
    {
        private DateTime _expirationDate;

        protected override ICacheItem<int> CreateCacheItem()
        {
            _expirationDate = Now.AddDays(1); 
            return new AbsoluteExpirationCacheItem<int>(Value, _expirationDate);
        }

        [Test]
        public void IsExpired_CurentDateTimeIsLessThanExpirationDate_ReturnsFalse()
        {
            bool isExpired = CacheItem.IsExpired(Now);

            Assert.False(isExpired);
        }


        [Test]
        public void IsExpired_CurentDateTimeIsGreaterThanExpirationDate_ReturnsTrue()
        {
            Now = _expirationDate.AddDays(1);
            bool isExpired = CacheItem.IsExpired(Now);

            Assert.True(isExpired);
        }
    }
}