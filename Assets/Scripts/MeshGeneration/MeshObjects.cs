using Intellectika.Geometry;
using UnityEngine;

namespace Intellectika
{
    internal class MeshObjects : MonoBehaviour
    {
        [SerializeField] private protected float Length = 4;
        [SerializeField] private protected float Width = 4;
        [SerializeField] private protected float Height = 4;
        private protected ObjectData data;
        [SerializeField] private protected Color color;
        [SerializeField] private protected float Radius = 4f;
        private protected float CurrentAngleX = 0;
        private protected float SingleAngle = 0;
        [SerializeField, Range(3, 50)] private protected int PlaneCount = 3;
        private protected int MeshIndex = 0;

        private protected MeshFilter[] mFilters;
        private protected MeshRenderer[] mRenderers;


        private protected Vector3 Point0;
        private protected Vector3 Point1;

        private protected Vector3 Center;

        [Range(2, 256)]
        [SerializeField] private protected  int Resolution = 8;

        private protected virtual void Init()
        {

            SingleAngle = 360 / (float)PlaneCount;
            mFilters = new MeshFilter[PlaneCount * 3];
            mRenderers = new MeshRenderer[PlaneCount * 3];
            Center = new Vector3 (0 , 0, 0);
        }


        private protected void ChangeColor(MeshRenderer[] _mRenderers, Color _color)
        {
            for (int x = 0; x < _mRenderers.Length; x++)
            {
                _mRenderers[x].material = new Material(Shader.Find("Standard"));
                _mRenderers[x].material.color = _color;
            }
        }

        private protected void Normalize(MeshFilter[] _mFilters, float _Radius)
        {
            for (int x = 0; x < _mFilters.Length; x++)
            {
                Vector3[] NewVertices = _mFilters[x].mesh.vertices;
                for (int i = 0; i < _mFilters[x].mesh.vertices.Length; i++)
                {
                    NewVertices[i] = (NewVertices[i].normalized * _Radius) ;
                }
                _mFilters[x].mesh.vertices = NewVertices;
            }
        }

        private protected void RecalculateNormals(MeshFilter[] _mFilters)
        {
            for (int x = 0; x < _mFilters.Length; x++)
            {
                _mFilters[x].mesh.RecalculateBounds();  // границы
                _mFilters[x].mesh.RecalculateNormals();  // нормали
            }
        }

        private protected void RecalculateSize(MeshFilter[] _mFilters, float SizeX, float SizeY, float SizeZ)
        {
            for (int x = 0; x < _mFilters.Length; x++)
            {
                Vector3[] NewVertices = _mFilters[x].mesh.vertices;
                for (int i = 0; i < _mFilters[x].mesh.vertices.Length; i++)
                {
                    NewVertices[i].x = NewVertices[i].x * SizeX;
                    NewVertices[i].y = NewVertices[i].y * SizeY;
                    NewVertices[i].z = NewVertices[i].z * SizeZ;
                }
                _mFilters[x].mesh.vertices = NewVertices;
            }
        }

        private protected void CalculatePoints()
        {
            Point0 = new Vector3(geometry.cos(45) * geometry.cos(CurrentAngleX - SingleAngle / 2), -.5f, geometry.cos(45) * geometry.sin(CurrentAngleX - SingleAngle / 2));
            Point1 = new Vector3(geometry.cos(45) * geometry.cos(CurrentAngleX + SingleAngle / 2), .5f, geometry.cos(45) * geometry.sin(CurrentAngleX + SingleAngle / 2));
            CurrentAngleX += SingleAngle;
        }

        private protected void CalculatePointsSphere()
        {
            Point0 = new Vector3(geometry.cos(CurrentAngleX - SingleAngle / 2), -.5f, geometry.sin(CurrentAngleX - SingleAngle / 2));
            Point1 = new Vector3(geometry.cos(CurrentAngleX + SingleAngle / 2), .5f, geometry.sin(CurrentAngleX + SingleAngle / 2));
            CurrentAngleX += SingleAngle;
        }

        private protected void RecalculatePosition(MeshFilter[] _mFilters)
        {
            for (int x = 0; x < _mFilters.Length; x++)
            {
                Vector3[] NewVertices = _mFilters[x].mesh.vertices;
                for (int i = 0; i < _mFilters[x].mesh.vertices.Length; i++)
                {
                    NewVertices[i] = NewVertices[i] + transform.position;
                }
                _mFilters[x].mesh.vertices = NewVertices;
            }
        }

        private protected void ChangePrismSize(MeshFilter[] _mFilters, float _Radius)
        {
            for (int x = 0; x < _mFilters.Length; x++)
            {
                Vector3[] NewVertices = _mFilters[x].mesh.vertices;
                for (int i = 0; i < _mFilters[x].mesh.vertices.Length; i++)
                {
                    NewVertices[i].x = NewVertices[i].x * _Radius;
                    NewVertices[i].z = NewVertices[i].z * _Radius;
                }
                _mFilters[x].mesh.vertices = NewVertices;
            }
        }
    }
}
