using UnityEngine;
namespace Graf.Properties
{
    public class FloatHashed
    {
        string key;

        public FloatHashed(string key)
        {
            this.key = key;
        }

        public float Value
        {
            get
            {
                float temp;
                float.TryParse(PlayerPrefs.GetString($"FLOAT_HASHED_{key}"), out temp);
                return temp;
            }
            set
            {
                PlayerPrefs.SetString($"FLOAT_HASHED_{key}", value.ToString());
            }
        }


    }
}