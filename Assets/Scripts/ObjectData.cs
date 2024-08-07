using Intellectika.Interfaces;
using UnityEngine;

namespace Intellectika
{
    internal class ObjectData : MeshObjects,ILocatable // берёт информцию из интерфейса для создания мешобъектов
    {
        public Panels sphere;
        public Panels prism;
        public Panels paral;
        public Panels capsule;
        public static ObjectData Data;
        string Name = "ObjectData";
        int ColorValue = 0;
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
            SetColor(ColorValue);
            Radius = float.Parse(sphere.Radius.text);
            PlaneCount =  int.Parse(sphere.PlaneCount.text);
            Resolution = int.Parse(sphere.Resolution.text);
        }

        public void GetDataForPrism()
        {
            SetColor(ColorValue);
            Radius = float.Parse(prism.Radius.text);
            PlaneCount = int.Parse(prism.PlaneCount.text);
            Resolution = int.Parse(prism.Resolution.text);
            Height = float.Parse(prism.Height.text);
        }

        public void GetDataForparal()
        {
            SetColor(ColorValue);
            PlaneCount = 4;
            Resolution = 2;
            Length = float.Parse(paral.Length.text);
            Height = float.Parse(paral.Height.text);
            Width = float.Parse(paral.Width.text);
        }

        public void GetDataForCapsule()
        {
            SetColor(ColorValue);
            Radius = float.Parse(capsule.Radius.text);
            PlaneCount = int.Parse(capsule.PlaneCount.text);
            Resolution = int.Parse(capsule.Resolution.text);
            Height = float.Parse(capsule.Height.text);
        }


        public void changeColorValueSphere()
        {
            ColorValue = sphere.drop.value;
        }
        public void changeColorValuePrism()
        {
            ColorValue = prism.drop.value;
        }
        public void changeColorValueParal()
        {
            ColorValue = paral.drop.value;
        }
        public void changeColorValueCapsule()
        {
            ColorValue = capsule.drop.value;
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
        public float GetHeght()
        {
            return Height;
        }
        public float GetLength()
        {
            return Length;
        }
        public float GetWidth()
        {
            return Width;
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

        public void SetColor(int v)
        {
            switch (v)
            {
                case 0:
                    color = Color.red; break;
                case 1:
                    color = Color.cyan; break;
                case 2:
                    color = Color.magenta; break;
                case 3:
                    color = Color.black; break;
                case 4:
                    color = Color.blue; break;
                case 5:
                    color = Color.green; break;
                case 6:
                    color = Color.gray; break;
                case 7:
                    color = Color.white; break;
                case 8:
                    color = Color.yellow; break;
            }
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
