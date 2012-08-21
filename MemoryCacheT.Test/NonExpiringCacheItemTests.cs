using System;
using NUnit.Framework;

namespace MemoryCacheT.Test
{
    [TestFixture]
    public class NonExpiringCacheItemTests : CacheItemTestBase
    {
        [Test]
        public void IsExpired_CurrentTimeIsPast_ReturnsFalse()
        {
            bool isExpired = CacheItem.IsExpired(DateTime.MinValue);

            Assert.False(isExpired);
        }
    }
}