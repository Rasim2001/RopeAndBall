using UnityEngine;
namespace Graf.Properties
{
    public class BoolHashed
    {
        string key;

        public BoolHashed(string key)
        {
            this.key = key;
        }

        public bool Value
        {
            get
            {
                bool temp;
                bool.TryParse(PlayerPrefs.GetString($"BOOL_HASHED_{key}"), out temp);
                return temp;
            }
            set
            {
                PlayerPrefs.SetString($"BOOL_HASHED_{key}", value.ToString());
            }
        }
    }
}