using Intellectika.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    internal class ObjectData : MeshObjects,ILocatable
    {
        public Panels sphere;
        public Panels prism;
        public Panels paral;
        public Panels capsule;
        public static ObjectData Data;
        string Name = "ObjectData";
        private void Awake()
        {
            if (data == null)
            {
                data = this;
                DontDestroyOnLoad(this);
                Intellectika.BootstrapSpace.LocatorReference.Locator.Add(Name, this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void GetDataForSphere()
        {
            Radius = float.Parse(sphere.Radius.text);
            PlaneCount =  int.Parse(sphere.PlaneCount.text);
            Resolution = int.Parse(sphere.Resolution.text);
        }

        public float GetRadius()
        {
            return Radius;
        }

        public void SetRadius(float v)
        {
            Radius = v;
        }

        public int GetPlaneCount()
        {
            return PlaneCount;
        }

        public void SetPlaneCount(int v)
        {
            PlaneCount = v;
        }

        public int GetResolution()
        {
            return Resolution;
        }

        public void SetResolution(int v)
        {
            Resolution = v;
        }

        public Color GetColor()
        {
            return color;
        }

        public void SetColor(Color v)
        {
            color = v;
        }

        public GameObject Return()
        {
            return gameObject;
        }

        public void OnDestroy()
        {
            Intellectika.BootstrapSpace.LocatorReference.Locator.Remove(Name);
        }
    }
}
