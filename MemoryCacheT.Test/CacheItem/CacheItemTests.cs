using NUnit.Framework;

namespace MemoryCacheT.Test.CacheItem
{
    [TestFixture]
    internal class CacheItemTests : CacheItemTestBase
    {
        [Test]
        public void Expire_OnExpireIsAssigned_DelegateCalled()
        {
            _dateTimeProviderMock.SetupGet(item => item.UtcNow).Returns(_now);
            bool isOnExpireCalled = false;
            _cacheItem.OnExpire = (value, time) => isOnExpireCalled = true;
            
            _cacheItem.Expire();

            Assert.True(isOnExpireCalled);
        }

        [Test]
        public void Expire_OnExpireIsNotAssigned_NoExceptions()
        {
            _cacheItem.Expire();
        }
        
        [Test]
        public void Remove_OnRemoveIsAssigned_DelegateCalled()
        {
            _dateTimeProviderMock.SetupGet(item => item.UtcNow).Returns(_now); 
            
            bool isOnRemoveCalled = false;
            _cacheItem.OnRemove = (value, time) => isOnRemoveCalled = true;
            
            _cacheItem.Remove();

            Assert.True(isOnRemoveCalled);
        }

        [Test]
        public void Remove_OnRemoveIsNotAssigned_NoExceptions()
        {
            _cacheItem.Remove();
        }

    }
}