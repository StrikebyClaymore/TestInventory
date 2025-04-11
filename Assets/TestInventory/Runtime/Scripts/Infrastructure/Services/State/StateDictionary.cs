using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace TestInventory
{
    [Serializable]
    public class StateDictionary<TKey, TValue> : ISerializationCallbackReceiver, IDeserializationCallback, ISerializable, IReadOnlyDictionary<TKey, TValue>
    {
        private Dictionary<TKey, TValue> _dictionary;
        [SerializeField] private TKey[] _keys;
        [SerializeField] private TValue[] _values;

        public StateDictionary()
        {
            _dictionary = new();
        }

        #region ISerializationCallbackReceiver

        public void OnAfterDeserialize()
        {
            if (_keys != null && _values != null && _keys.Length == _values.Length)
            {
                _dictionary.Clear();
                for (int i = 0; i < _keys.Length; ++i)
                    _dictionary[_keys[i]] = GetValue(_values, i);

                _keys = null;
                _values = null;
            }
        }

        public void OnBeforeSerialize()
        {
            var length = _dictionary.Count;
            _keys = new TKey[length];
            _values = new TValue[length];

            var i = 0;
            foreach (var kvp in _dictionary)
            {
                _keys[i] = kvp.Key;
                SetValue(_values, i, kvp.Value);
                ++i;
            }
        }

        #endregion
        
        #region IDeserializationCallback
        
        public void OnDeserialization(object sender)
        {
            ((IDeserializationCallback)_dictionary).OnDeserialization(sender);
        }
        
        #endregion

        #region ISerializable
        
        private StateDictionary(SerializationInfo info, StreamingContext context)
        {
            _dictionary = new();
            var keys = (TKey[])info.GetValue("keys", typeof(TKey[]));
            var values = (TValue[])info.GetValue("values", typeof(TValue[]));
            for (int i = 0; i < keys.Length; i++)
            {
                _dictionary[keys[i]] = values[i];
            }
        }

        public void GetObjectData (SerializationInfo info, StreamingContext context)
        {
            var keys = new TKey[_dictionary.Count];
            var values = new TValue[_dictionary.Count];
            int i = 0;
            foreach (var kvp in _dictionary)
            {
                keys[i] = kvp.Key;
                values[i] = kvp.Value;
                i++;
            }
            info.AddValue("keys", keys);
            info.AddValue("values", values);
        }
        
        #endregion

        #region Dictionary
        
        public int Count => _dictionary.Count;
        public ICollection<TKey> Keys => _dictionary.Keys;
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;
        public ICollection<TValue> Values => _dictionary.Values;
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;
        
        public TValue this [TKey key]
        {
            get => ((IDictionary<TKey, TValue>)_dictionary)[key];
            set => ((IDictionary<TKey, TValue>)_dictionary)[key] = value;
        }

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
        }

        public void Remove(TKey key)
        {
            _dictionary.Remove(key);
        }

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }
        
        public TValue Get(TKey key)
        {
            return _dictionary[key];
        }

        public void Set(TKey key,  TValue value)
        {
            _dictionary[key] = value;
        }
        
        public void Clear()
        {
            _dictionary.Clear();
        }
        
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        #endregion
        
        private TValue GetValue (TValue[] storage, int i)
        {
            return storage[i];
        }

        private void SetValue (TValue[] storage, int i, TValue value)
        {
            storage[i] = value;
        }
    }
}