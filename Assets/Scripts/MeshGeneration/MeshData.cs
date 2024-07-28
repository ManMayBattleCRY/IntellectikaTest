using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    public class MeshData : MonoBehaviour
    {
        [HideInInspector]public int TriangleIndex = 0;
        [HideInInspector] public int VerticeIndex = 0;
        [HideInInspector] public int[] Triangels;
        [HideInInspector] public Vector3[] Vertices;

        public struct PlaneVectors
        {
            public Vector3 v00;
            public Vector3 v01;
            public Vector3 v10;
            public Vector3 v11;
        };
        public PlaneVectors _PlaneVectors;
    }
}
