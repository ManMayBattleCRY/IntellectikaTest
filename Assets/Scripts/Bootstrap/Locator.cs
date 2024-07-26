using Intellectika.Terrain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Intellectika.Interfaces;

namespace Intellectika.BootstrapSpace
{
    public class Locator : MonoBehaviour
    {

        public static Locator LocatorInstance { get; private set; }
        Dictionary<string,ILocatable> ReferenceStorage = new Dictionary<string,Interfaces.ILocatable>();

        public void Add(string name, ILocatable obj)
        {
            ReferenceStorage.Add(name, obj);
        }

        public GameObject Get(string name) 
        {
            if (ReferenceStorage[name] != null) return ReferenceStorage[name].Return();
            else return null;
        }

        public void Remove(string name) 
        {  
            ReferenceStorage.Remove(name); 
        }



        private void Awake()
        {
            if (LocatorInstance == null)
            {
                LocatorInstance = this;
                DontDestroyOnLoad(this);
            }
            else Destroy(gameObject);
        }
    }
}
