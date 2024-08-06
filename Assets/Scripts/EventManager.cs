using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    public class EventManagerReference : MonoBehaviour
    {
        public static EventManagerReference EventManager;
        Dictionary<string, Event> Events = new Dictionary<string, Event>();
        private void Awake()
        {
            if (EventManager == null)
            {
                EventManager = this;
                DontDestroyOnLoad(this);
            }
            else Destroy(gameObject);
        }

        public void Subscribe()
        { 
        }

        public void Dispose()
        { 
        }
    }
}
