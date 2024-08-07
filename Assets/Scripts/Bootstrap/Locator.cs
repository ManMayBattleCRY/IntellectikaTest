using Intellectika.Terrain;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Intellectika.Interfaces;

namespace Intellectika.BootstrapSpace
{
    public class LocatorReference : MonoBehaviour  // Локатор содержит ссылкы на важные объекты
    {

        public static LocatorReference Locator { get; private set; }
        Dictionary<string,ILocatable> ReferenceStorage = new Dictionary<string,Interfaces.ILocatable>();

        public void Add(string _name, ILocatable obj)
        {
            ReferenceStorage.Add(_name, obj);
        }

        public GameObject Get(string _name) 
        {
            if (ReferenceStorage[_name] != null) return ReferenceStorage[_name].Return();
            else return null;
        }

        public void Remove(string _name) 
        {  
            ReferenceStorage.Remove(_name); 
        }



        private void Awake()
        {
            if (Locator == null)
            {
                Locator = this;
                DontDestroyOnLoad(this);
            }
            else Destroy(gameObject);
        }
    }
}
