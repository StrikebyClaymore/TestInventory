using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestInventory
{
    [Serializable]
    public class SaveState : ISerializationCallbackReceiver
    {
        protected readonly Dictionary<string, object> _objectDictionary = new(StringComparer.Ordinal);
        [SerializeField] private StateDictionary<string, string> _jsonDictionary = new();
        
        public void SetState<TState> (TState state, string instanceId = default) where TState : class
        {
            var key = state.GetType().AssemblyQualifiedName;
            if (!string.IsNullOrEmpty(instanceId))
                key += $", InstanceID={instanceId}";
            _objectDictionary[key] = state;
        }
        
        public TState GetState<TState> (string instanceId = default) where TState : class
        {
            var key = typeof(TState).AssemblyQualifiedName;
            if (!string.IsNullOrEmpty(instanceId))
                key += $", InstanceID={instanceId}";
            return _objectDictionary.TryGetValue(key, out var state) ? state as TState : null;
        }
        
        public void OnBeforeSerialize()
        {
            _jsonDictionary.Clear();
            foreach (var kv in _objectDictionary)
                _jsonDictionary.Add(kv.Key, JsonUtility.ToJson(kv.Value));
        }

        public void OnAfterDeserialize()
        {
            _objectDictionary.Clear();
            foreach (var kv in _jsonDictionary)
            {
                var type = Type.GetType(kv.Key);
                if (type is null)
                    continue;
                _objectDictionary[kv.Key] = JsonUtility.FromJson(kv.Value, type);
            }
        }
    }
}