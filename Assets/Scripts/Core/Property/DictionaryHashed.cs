using System.Collections.Generic;
using UnityEngine;
namespace Graf.Properties
{
    public class DictionaryHashed<TKey, TValue>
    {
        string key;

        public DictionaryHashed(string key)
        {
            this.key = key;
        }

        public Dictionary<TKey, TValue> Value
        {
            get
            {
                return JsonUtility.FromJson<Dictionary<TKey, TValue>>(PlayerPrefs.GetString($"DICTIONARY_HASHED_{key}"));
            }
        }

        public void Add(TKey key, TValue value)
        {
            var temp = Value;
            temp.Add(key, value);
            PlayerPrefs.SetString($"DICTIONARY_HASHED_{key}", JsonUtility.ToJson(temp));
        }
    }
}