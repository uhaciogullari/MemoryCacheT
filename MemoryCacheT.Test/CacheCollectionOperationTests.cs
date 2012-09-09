using System;
using NUnit.Framework;

namespace MemoryCacheT.Test
{
    [TestFixture]
    internal class CacheCollectionOperationTests : CacheTestBase
    {
        //Add
        [Test]
        public void Add_NullKey_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.Add(null, _cacheItem));
        }

        [Test]
        public void Add_NullCacheItem_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.Add(_key, null));
        }

        [Test]
        public void Add_KeyDoesntExist_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);

            Assert.True(_cache.Count > default(int));
        }

        [Test]
        public void Add_KeyExists_ReturnsFalse()
        {
            _cache.Add(_key, _cacheItem);

            Assert.Throws<ArgumentException>(() => _cache.Add(_key, new NonExpiringCacheItem<int>(_value)));
        }


        //TryAdd
        [Test]
        public void TryAdd_NullKey_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.TryAdd(null, _cacheItem));
        }

        [Test]
        public void TryAdd_NullCacheItem_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.TryAdd(_key, null));
        }

        [Test]
        public void TryAdd_KeyDoesntExist_ReturnsTrue()
        {
            bool isAdded = _cache.TryAdd(_key, _cacheItem);

            Assert.True(isAdded);
        }

        [Test]
        public void TryAdd_KeyExists_ReturnsFalse()
        {
            _cache.Add(_key, _cacheItem);
            bool isAdded = _cache.TryAdd(_key, _cacheItem);

            Assert.False(isAdded);
        }

        [Test]
        public void TryAdd_KeyExists_ItemIsNotAdded()
        {
            _cache.Add(_key, _cacheItem);
            _cache.TryAdd(_key, _cacheItem);

            Assert.AreEqual(1, _cache.Count);
        }


        //TryGet
        [Test]
        public void TryGet_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);
            int value;
            bool result = _cache.TryGetValue(_key, out value);

            Assert.True(result);
        }

        [Test]
        public void TryGet_KeyDoesNotExist_ReturnFalse()
        {
            int value;
            bool result = _cache.TryGetValue(_key, out value);

            Assert.False(result);
        }

        [Test]
        public void TryGet_NullKey_ThrowsException()
        {
            int value;
            Assert.Throws<ArgumentNullException>(() => _cache.TryGetValue(null, out value));
        }


        //TryGetCacheItem
        [Test]
        public void TryGetCacheItem_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);
            ICacheItem<int> value;
            bool result = _cache.TryGetValue(_key, out value);

            Assert.True(result);
        }

        [Test]
        public void TryGetCacheItem_KeyDoesNotExist_ReturnFalse()
        {
            ICacheItem<int> value;
            bool result = _cache.TryGetValue(_key, out value);

            Assert.False(result);
        }

        [Test]
        public void TryGetCacheItem_NullKey_ThrowsException()
        {
            ICacheItem<int> value;
            Assert.Throws<ArgumentNullException>(() => _cache.TryGetValue(null, out value));
        }


        //TryUpdate
        [Test]
        public void TryUpdate_KeyExists_ReturnsTrue()
        {
            int newValue = _value + 7;
            _cache.Add(_key, _cacheItem);

            bool result = _cache.TryUpdate(_key, newValue);

            Assert.True(result);
        }

        [Test]
        public void TryUpdate_KeyExists_ValueIsUpdated()
        {
            int newValue = _value + 7;
            _cache.Add(_key, _cacheItem);
            _cache.TryUpdate(_key, newValue);

            int updatedValue;
            bool result = _cache.TryGetValue(_key, out updatedValue) && updatedValue == newValue;

            Assert.True(result);
        }

        [Test]
        public void TryUpdate_KeyDoesNotExist_ReturnsFalse()
        {
            bool result = _cache.TryUpdate(_key, _value);

            Assert.False(result);
        }

        [Test]
        public void TryUpdate_KeyDoesNotExist_ValueIsNotUpdated()
        {
            int newValue = _value + 7;
            _cache.Add(_key, _cacheItem);
            _cache.TryUpdate("invalidKey", newValue);

            int updatedValue;
            bool result = _cache.TryGetValue(_key, out updatedValue) && updatedValue == _value;

            Assert.True(result);
        }

        [Test]
        public void TryUpdate_KeyDoesNotExist_NewItemIsNotAdded()
        {
            _cache.TryUpdate(_key, _value);

            int updatedValue;
            bool result = _cache.TryGetValue(_key, out updatedValue);

            Assert.False(result);
        }


        //TryUpdateCacheItem
        [Test]
        public void TryUpdateCacheItem_KeyExists_ReturnsTrue()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);
            _cache.Add(_key, _cacheItem);

            bool result = _cache.TryUpdate(_key, newCacheItem);

            Assert.True(result);
        }

        [Test]
        public void TryUpdateCacheItem_KeyExists_ValueIsUpdated()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);
            _cache.Add(_key, _cacheItem);
            _cache.TryUpdate(_key, newCacheItem);

            ICacheItem<int> updatedCacheItem;
            bool result = _cache.TryGetValue(_key, out updatedCacheItem) && updatedCacheItem == newCacheItem;

            Assert.True(result);
        }

        [Test]
        public void TryUpdateCacheItem_KeyDoesNotExist_ReturnsFalse()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);
            bool result = _cache.TryUpdate(_key, newCacheItem);

            Assert.False(result);
        }

        [Test]
        public void TryUpdateCacheItem_KeyDoesNotExist_ValueIsNotUpdated()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);

            _cache.Add(_key, _cacheItem);
            _cache.TryUpdate("invalidKey", newCacheItem);

            ICacheItem<int> updatedCacheItem;
            bool result = _cache.TryGetValue(_key, out updatedCacheItem) && updatedCacheItem == _cacheItem;

            Assert.True(result);
        }

        [Test]
        public void TryUpdateCacheItem_KeyDoesNotExist_NewItemIsNotAdded()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);
            _cache.TryUpdate(_key, newCacheItem);

            ICacheItem<int> updatedCacheItem;
            bool result = _cache.TryGetValue(_key, out updatedCacheItem);

            Assert.False(result);
        }


        //Clear
        [Test]
        public void Clear_CacheIsNotEmpty_AllItemsAreRemoved()
        {
            _cache.Add("1", new NonExpiringCacheItem<int>(1));
            _cache.Add("2", new NonExpiringCacheItem<int>(2));
            _cache.Add("3", new NonExpiringCacheItem<int>(3));

            _cache.Clear();

            Assert.AreEqual(default(int), _cache.Count);
        }

        [Test]
        public void Clear_CacheIsNotEmpty_OnRemoveIsCalled()
        {
            bool onRemoveIsCalled = false;
            _cacheItem.OnRemove = (i, time) => onRemoveIsCalled = true;
            _cache.Add(_key, _cacheItem);

            _cache.Clear();

            Assert.True(onRemoveIsCalled);
        }


        //TryRemove
        [Test]
        public void TryRemove_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);

            bool result = _cache.TryRemove(_key);

            Assert.True(result);
        }

        [Test]
        public void TryRemove_KeyDoesNotExist_ReturnsFalse()
        {
            bool result = _cache.TryRemove(_key);

            Assert.False(result);
        }


        //TryRemoveGetValue
        [Test]
        public void TryRemoveGetValue_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);

            int value;
            bool result = _cache.TryRemove(_key, out value);

            Assert.True(result);
        }

        [Test]
        public void TryRemoveGetValue_KeyExists_ReturnsCorrectValue()
        {
            _cache.Add(_key, _cacheItem);

            int value;
            _cache.TryRemove(_key, out value);

            Assert.AreEqual(_value, value);
        }

        [Test]
        public void TryRemoveGetValue_KeyDoesNotExist_ReturnsFalse()
        {
            int value;
            bool result = _cache.TryRemove(_key, out value);

            Assert.False(result);
        }


        //TryRemoveGetCacheItem
        [Test]
        public void TryRemoveGetCacheItem_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);

            ICacheItem<int> cacheItem;
            bool result = _cache.TryRemove(_key, out cacheItem);

            Assert.True(result);
        }

        [Test]
        public void TryRemoveGetCacheItem_KeyExists_ReturnsCorrectValue()
        {
            _cache.Add(_key, _cacheItem);

            ICacheItem<int> cacheItem;
            _cache.TryRemove(_key, out cacheItem);

            Assert.AreEqual(_cacheItem, cacheItem);
        }

        [Test]
        public void TryRemoveCacheItem_KeyDoesNotExist_ReturnsFalse()
        {
            ICacheItem<int> cacheItem;
            bool result = _cache.TryRemove(_key, out cacheItem);

            Assert.False(result);
        }


        //ContainsKey
        [Test]
        public void ContainsKey_KeyExists_ReturnsTrue()
        {
            _cache.Add(_key, _cacheItem);

            bool result = _cache.ContainsKey(_key);

            Assert.True(result);
        }

        [Test]
        public void ContainsKey_KeyDoesNotExist_ReturnsTrue()
        {
            bool result = _cache.ContainsKey(_key);

            Assert.False(result);
        }

        [Test]
        public void ContainsKey_NullKey_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _cache.ContainsKey(null));
        }

    }
}