using UnityEngine;

namespace Graf.Utils
{
   
    public class ResourceLoaderService<T> where T : Object
    {
        public T this[string name] => Resources.Load<T>(name);

        public PrefabsData Prefabs { get; private set; }
        public ResourceLoaderService()
        {
            Prefabs = Resources.Load<PrefabsData>("Scriptable Objects/PrefabsData");
        }
    }
}