using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace MemoryCacheT.Test
{
    [TestFixture]
    internal class CacheCollectionOperationTests : CacheTestBase
    {
        private int _value;
        private NonExpiringCacheItem<int> _cacheItem;
        private string _key;

        protected override void FinalizeSetup()
        {
            _value = 7;
            _key = "key";
            _cacheItem = new NonExpiringCacheItem<int>(_value);
        }

        //Add
        [Test]
        public void Add_NullKey_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Cache.Add(null, _cacheItem));
        }

        [Test]
        public void Add_NullCacheItem_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Cache.Add(_key, null));
        }

        [Test]
        public void Add_KeyDoesntExist_ReturnsTrue()
        {
            Cache.Add(_key, _cacheItem);

            Assert.True(Cache.Count > default(int));
        }

        [Test]
        public void Add_KeyExists_ReturnsFalse()
        {
            Cache.Add(_key, _cacheItem);

            Assert.Throws<ArgumentException>(() => Cache.Add(_key, new NonExpiringCacheItem<int>(_value)));
        }


        //TryAdd
        [Test]
        public void TryAdd_NullKey_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Cache.TryAdd(null, _cacheItem));
        }

        [Test]
        public void TryAdd_NullCacheItem_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Cache.TryAdd(_key, null));
        }

        [Test]
        public void TryAdd_KeyDoesntExist_ReturnsTrue()
        {
            bool isAdded = Cache.TryAdd(_key, _cacheItem);

            Assert.True(isAdded);
        }

        [Test]
        public void TryAdd_KeyExists_ReturnsFalse()
        {
            Cache.Add(_key, _cacheItem);
            bool isAdded = Cache.TryAdd(_key, _cacheItem);

            Assert.False(isAdded);
        }

        [Test]
        public void TryAdd_KeyExists_ItemIsNotAdded()
        {
            Cache.Add(_key, _cacheItem);
            Cache.TryAdd(_key, _cacheItem);

            Assert.AreEqual(1, Cache.Count);
        }


        //TryGet
        [Test]
        public void TryGet_KeyExists_ReturnsTrue()
        {
            Cache.Add(_key, _cacheItem);
            int value;
            bool result = Cache.TryGetValue(_key, out value);

            Assert.True(result);
        }

        [Test]
        public void TryGet_KeyDoesNotExist_ReturnFalse()
        {
            int value;
            bool result = Cache.TryGetValue(_key, out value);

            Assert.False(result);
        }

        [Test]
        public void TryGet_NullKey_ThrowsException()
        {
            int value;
            Assert.Throws<ArgumentNullException>(() => Cache.TryGetValue(null, out value));
        }


        //TryGetCacheItem
        [Test]
        public void TryGetCacheItem_KeyExists_ReturnsTrue()
        {
            Cache.Add(_key, _cacheItem);
            ICacheItem<int> value;
            bool result = Cache.TryGetValue(_key, out value);

            Assert.True(result);
        }

        [Test]
        public void TryGetCacheItem_KeyDoesNotExist_ReturnFalse()
        {
            ICacheItem<int> value;
            bool result = Cache.TryGetValue(_key, out value);

            Assert.False(result);
        }

        [Test]
        public void TryGetCacheItem_NullKey_ThrowsException()
        {
            ICacheItem<int> value;
            Assert.Throws<ArgumentNullException>(() => Cache.TryGetValue(null, out value));
        }


        //TryUpdate
        [Test]
        public void TryUpdate_KeyExists_ReturnsTrue()
        {
            int newValue = _value + 7;
            Cache.Add(_key, _cacheItem);

            bool result = Cache.TryUpdate(_key, newValue);

            Assert.True(result);
        }

        [Test]
        public void TryUpdate_KeyExists_ValueIsUpdated()
        {
            int newValue = _value + 7;
            Cache.Add(_key, _cacheItem);
            Cache.TryUpdate(_key, newValue);

            int updatedValue;
            bool result = Cache.TryGetValue(_key, out updatedValue) && updatedValue == newValue;

            Assert.True(result);
        }

        [Test]
        public void TryUpdate_KeyDoesNotExist_ReturnsFalse()
        {
            bool result = Cache.TryUpdate(_key, _value);

            Assert.False(result);
        }

        [Test]
        public void TryUpdate_KeyDoesNotExist_ValueIsNotUpdated()
        {
            int newValue = _value + 7;
            Cache.Add(_key, _cacheItem);
            Cache.TryUpdate("invalidKey", newValue);

            int updatedValue;
            bool result = Cache.TryGetValue(_key, out updatedValue) && updatedValue == _value;

            Assert.True(result);
        }

        [Test]
        public void TryUpdate_KeyDoesNotExist_NewItemIsNotAdded()
        {
            Cache.TryUpdate(_key, _value);

            int updatedValue;
            bool result = Cache.TryGetValue(_key, out updatedValue);

            Assert.False(result);
        }


        //TryUpdateCacheItem
        [Test]
        public void TryUpdateCacheItem_KeyExists_ReturnsTrue()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);
            Cache.Add(_key, _cacheItem);

            bool result = Cache.TryUpdate(_key, newCacheItem);

            Assert.True(result);
        }

        [Test]
        public void TryUpdateCacheItem_KeyExists_ValueIsUpdated()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);
            Cache.Add(_key, _cacheItem);
            Cache.TryUpdate(_key, newCacheItem);

            ICacheItem<int> updatedCacheItem;
            bool result = Cache.TryGetValue(_key, out updatedCacheItem) && updatedCacheItem == newCacheItem;

            Assert.True(result);
        }

        [Test]
        public void TryUpdateCacheItem_KeyDoesNotExist_ReturnsFalse()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);
            bool result = Cache.TryUpdate(_key, newCacheItem);

            Assert.False(result);
        }

        [Test]
        public void TryUpdateCacheItem_KeyDoesNotExist_ValueIsNotUpdated()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);

            Cache.Add(_key, _cacheItem);
            Cache.TryUpdate("invalidKey", newCacheItem);

            ICacheItem<int> updatedCacheItem;
            bool result = Cache.TryGetValue(_key, out updatedCacheItem) && updatedCacheItem == _cacheItem;

            Assert.True(result);
        }

        [Test]
        public void TryUpdateCacheItem_KeyDoesNotExist_NewItemIsNotAdded()
        {
            int newValue = _value + 7;
            ICacheItem<int> newCacheItem = new NonExpiringCacheItem<int>(newValue);
            Cache.TryUpdate(_key, newCacheItem);

            ICacheItem<int> updatedCacheItem;
            bool result = Cache.TryGetValue(_key, out updatedCacheItem);

            Assert.False(result);
        }


        //Clear
        [Test]
        public void Clear_CacheIsNotEmpty_AllItemsAreRemoved()
        {
            Cache.Add("1", new NonExpiringCacheItem<int>(1));
            Cache.Add("2", new NonExpiringCacheItem<int>(2));
            Cache.Add("3", new NonExpiringCacheItem<int>(3));

            Cache.Clear();

            Assert.AreEqual(default(int), Cache.Count);
        }

        [Test]
        public void Clear_CacheIsNotEmpty_OnRemoveIsCalled()
        {
            bool onRemoveIsCalled = false;
            _cacheItem.OnRemove = (i, time) => onRemoveIsCalled = true;
            Cache.Add(_key, _cacheItem);

            Cache.Clear();

            Assert.True(onRemoveIsCalled);
        }


        //TryRemove
        [Test]
        public void TryRemove_KeyExists_ReturnsTrue()
        {
            Cache.Add(_key, _cacheItem);

            bool result = Cache.TryRemove(_key);

            Assert.True(result);
        }

        [Test]
        public void TryRemove_KeyDoesNotExist_ReturnsFalse()
        {
            bool result = Cache.TryRemove(_key);

            Assert.False(result);
        }


        //TryRemoveGetValue
        [Test]
        public void TryRemoveGetValue_KeyExists_ReturnsTrue()
        {
            Cache.Add(_key, _cacheItem);

            int value;
            bool result = Cache.TryRemove(_key, out value);

            Assert.True(result);
        }

        [Test]
        public void TryRemoveGetValue_KeyExists_ReturnsCorrectValue()
        {
            Cache.Add(_key, _cacheItem);

            int value;
            Cache.TryRemove(_key, out value);

            Assert.AreEqual(_value, value);
        }

        [Test]
        public void TryRemoveGetValue_KeyDoesNotExist_ReturnsFalse()
        {
            int value;
            bool result = Cache.TryRemove(_key, out value);

            Assert.False(result);
        }


        //TryRemoveGetCacheItem
        [Test]
        public void TryRemoveGetCacheItem_KeyExists_ReturnsTrue()
        {
            Cache.Add(_key, _cacheItem);

            ICacheItem<int> cacheItem;
            bool result = Cache.TryRemove(_key, out cacheItem);

            Assert.True(result);
        }

        [Test]
        public void TryRemoveGetCacheItem_KeyExists_ReturnsCorrectValue()
        {
            Cache.Add(_key, _cacheItem);

            ICacheItem<int> cacheItem;
            Cache.TryRemove(_key, out cacheItem);

            Assert.AreEqual(_cacheItem, cacheItem);
        }

        [Test]
        public void TryRemoveCacheItem_KeyDoesNotExist_ReturnsFalse()
        {
            ICacheItem<int> cacheItem;
            bool result = Cache.TryRemove(_key, out cacheItem);

            Assert.False(result);
        }


        //ContainsKey
        [Test]
        public void ContainsKey_KeyExists_ReturnsTrue()
        {
            Cache.Add(_key, _cacheItem);

            bool result = Cache.ContainsKey(_key);

            Assert.True(result);
        }

        [Test]
        public void ContainsKey_KeyDoesNotExist_ReturnsTrue()
        {
            bool result = Cache.ContainsKey(_key);

            Assert.False(result);
        }

        [Test]
        public void ContainsKey_NullKey_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Cache.ContainsKey(null));
        }

    }
}