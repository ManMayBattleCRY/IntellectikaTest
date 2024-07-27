using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    abstract public class MeshCreator : MonoBehaviour
    {
       public Vector3[] Vertices;
       public int[] Triangles;
       public abstract Vector3[] CreateVertices(int X, int Y, int Z);

       public abstract void CreateTriangle(int X, int Y, int Z);
    }
}
