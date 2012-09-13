using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace MemoryCacheT.Test.CollectionOperations
{
    [TestFixture]
    internal class RemoveTests : CacheTestBase
    {
        private Mock<ICacheItem<int>> _cacheItemMock;

        protected override void FinalizeSetup()
        {
            _cacheItemMock = new Mock<ICacheItem<int>>(MockBehavior.Strict);
        }

        protected override void FinalizeTearDown()
        {
            _cacheItemMock.VerifyAll();
        }

        [Test]
        public void RemoveKeyValuePair_KeyValuePairExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItemMock.Object);
            _cacheItemMock.SetupGet(mock => mock.Value).Returns(_value);
            _cacheItemMock.Setup(mock => mock.Remove()).Verifiable();

            bool result = _cache.Remove(new KeyValuePair<string, int>(_key, _value));

            Assert.True(result);
        }

        [Test]
        public void RemoveKeyValuePair_KeyDoesntExist_ReturnsFalse()
        {
            bool result = _cache.Remove(new KeyValuePair<string, int>(_key, _value));

            Assert.False(result);
        }

        [Test]
        public void RemoveKeyValuePair_ValueDoesntMatch_ReturnsFalse()
        {
            _cache.Add(_key, _cacheItemMock.Object);
            _cacheItemMock.SetupGet(mock => mock.Value).Returns(_value);

            bool result = _cache.Remove(new KeyValuePair<string, int>(_key, _value + 1));

            Assert.False(result);
        }

        [Test]
        public void RemoveKeyValuePair_NullKey_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.Remove(new KeyValuePair<string, int>(null, _value)));
        }


        [Test]
        public void RemoveByKey_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItemMock.Object);
            _cacheItemMock.Setup(mock => mock.Remove()).Verifiable();

            bool result = _cache.Remove(_key);

            Assert.True(result);
        }

        [Test]
        public void RemoveByKey_KeyDoesNotExist_ReturnsFalse()
        {
            bool result = _cache.Remove(_key);

            Assert.False(result);
        }

        [Test]
        public void RemoveByKey_NullKey_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.Remove(null));
        }


        [Test]
        public void RemoveGetValue_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItemMock.Object);
            _cacheItemMock.SetupGet(mock => mock.Value).Returns(_value);
            _cacheItemMock.Setup(mock => mock.Remove()).Verifiable();

            int value;
            bool result = _cache.Remove(_key, out value);

            Assert.True(result);
        }

        [Test]
        public void RemoveGetValue_KeyExists_ReturnsCorrectValue()
        {
            _cache.Add(_key, _cacheItemMock.Object);
            _cacheItemMock.SetupGet(mock => mock.Value).Returns(_value);
            _cacheItemMock.Setup(mock => mock.Remove()).Verifiable();

            int value;
            _cache.Remove(_key, out value);

            Assert.AreEqual(_value, value);
        }

        [Test]
        public void RemoveGetValue_KeyDoesNotExist_ReturnsFalse()
        {
            int value;
            bool result = _cache.Remove(_key, out value);

            Assert.False(result);
        }

        [Test]
        public void RemoveGetCacheItem_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItemMock.Object);
            _cacheItemMock.Setup(mock => mock.Remove()).Verifiable();

            ICacheItem<int> cacheItem;
            bool result = _cache.Remove(_key, out cacheItem);

            Assert.True(result);
        }

        [Test]
        public void RemoveGetCacheItem_KeyExists_ReturnsCorrectValue()
        {
            _cache.Add(_key, _cacheItemMock.Object);
            _cacheItemMock.Setup(mock => mock.Remove()).Verifiable();

            ICacheItem<int> cacheItem;
            _cache.Remove(_key, out cacheItem);

            Assert.AreEqual(_cacheItemMock.Object, cacheItem);
        }

        [Test]
        public void RemoveCacheItem_KeyDoesNotExist_ReturnsFalse()
        {
            ICacheItem<int> cacheItem;
            bool result = _cache.Remove(_key, out cacheItem);

            Assert.False(result);
        }

        [Test]
        public void RemoveCacheItem_NullKey_ThrowsException()
        {
            ICacheItem<int> cacheItem;

            Assert.Throws<ArgumentNullException>(() => _cache.Remove(null, out cacheItem));
        }


    }
}