using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Graf.Properties
{
    public class ListHashed
    {
        string key;

        public ListHashed(string key)
        {
            this.key = key;
        }

        public List<string> Value
        {
            get
            {
                List<string> temp;
                temp = PlayerPrefs.GetString($"LIST_HASHED_{key}").Split(',').ToList();
                if (temp.Count == 1 && temp[0] == "")
                    temp.RemoveAt(0);
                return temp;
            }
        }

        public void Add(string value) 
        {
            var temp = Value;
            temp.Add(value);
            PlayerPrefs.SetString($"LIST_HASHED_{key}", string.Join(",", temp.ToArray()));
        }
    }
}