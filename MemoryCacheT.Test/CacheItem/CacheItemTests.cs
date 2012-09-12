using NUnit.Framework;

namespace MemoryCacheT.Test.CacheItem
{
    [TestFixture]
    internal class CacheItemTests : CacheItemTestBase
    {
        [Test]
        public void Expire_OnExpireIsAssigned_DelegateCalled()
        {
            DateTimeProviderMock.SetupGet(item => item.Now).Returns(Now);
            bool isOnExpireCalled = false;
            CacheItem.OnExpire = (value, time) => isOnExpireCalled = true;
            
            CacheItem.Expire();

            Assert.True(isOnExpireCalled);
        }

        [Test]
        public void Expire_OnExpireIsNotAssigned_NoExceptions()
        {
            CacheItem.Expire();
        }
        
        [Test]
        public void Remove_OnRemoveIsAssigned_DelegateCalled()
        {
            DateTimeProviderMock.SetupGet(item => item.Now).Returns(Now); 
            
            bool isOnRemoveCalled = false;
            CacheItem.OnRemove = (value, time) => isOnRemoveCalled = true;
            
            CacheItem.Remove();

            Assert.True(isOnRemoveCalled);
        }

        [Test]
        public void Remove_OnRemoveIsNotAssigned_NoExceptions()
        {
            CacheItem.Remove();
        }

    }
}