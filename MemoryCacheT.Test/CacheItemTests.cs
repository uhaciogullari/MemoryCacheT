using NUnit.Framework;

namespace MemoryCacheT.Test
{
    [TestFixture]
    public class CacheItemTests : CacheItemTestBase
    {
        [Test]
        public void Expire_OnExpireIsAssigned_DelegateCalled()
        {
            bool isOnExpireCalled = false;
            CacheItem.OnExpire = (value, time) => isOnExpireCalled = true;
            
            CacheItem.Expire(Now);

            Assert.True(isOnExpireCalled);
        }
        
        [Test]
        public void Remove_OnRemoveIsAssigned_DelegateCalled()
        {
            bool isOnRemoveCalled = false;
            CacheItem.OnRemove = (value, time) => isOnRemoveCalled = true;
            
            CacheItem.Remove(Now);

            Assert.True(isOnRemoveCalled);
        }

    }
}