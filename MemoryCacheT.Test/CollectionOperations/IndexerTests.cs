using System.Collections.Generic;
using NUnit.Framework;

namespace MemoryCacheT.Test.CollectionOperations
{
    [TestFixture]
    internal class IndexerTests : CacheTestBase
    {
        protected override void FinalizeSetup()
        {
            _cache = new Cache<string, int>(_timerMock.Object, _timerInterval, null, _cacheItemFactoryMock.Object);
        }

        [Test]
        public void IndexerSet_KeyDoesNotExist_AssignsValue()
        {
            _cacheItemFactoryMock.Setup(item => item.CreateInstance(_value)).Returns(_cacheItem);

            _cache[_key] = _value;

            Assert.AreEqual(_value, _cache[_key]);
        }

        [Test]
        public void IndexerSet_KeyExists_OverwritesOldValue()
        {
            int newValue = _value + 7;
            _cacheItemFactoryMock.Setup(item => item.CreateInstance(newValue)).Returns(new NonExpiringCacheItem<int>(newValue));

            _cache.Add(_key, _cacheItem);
            _cache[_key] = newValue;

            Assert.AreEqual(newValue, _cache[_key]);
        }

        [Test]
        public void IndexerGet_KeyDoesNotExist_ThrowsException()
        {
            Assert.Throws<KeyNotFoundException>(() => { int i = _cache[_key]; });
        }
    }
}