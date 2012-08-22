using NUnit.Framework;

namespace MemoryCacheT.Test
{
    [TestFixture]
    internal class NonExpiringCacheItemTests : CacheItemTestBase
    {
        [Test]
        public void IsExpired_CurrentTimeIsPast_ReturnsFalse()
        {
            bool isExpired = CacheItem.IsExpired();

            Assert.False(isExpired);
        }
    }
}