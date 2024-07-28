using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using geometry = Intellectika.Geometry.geometry;

namespace Intellectika
{
    [RequireComponent(typeof(PlaneCreator))]
    public class WrapCreator : MeshData
    {
        Mesh mesh;
        public int Radius = 5;
        public int Height = 4;
        public int StepX = 1;
        public int StepY = 1;
        public int PlaneCount;
        float SingleAngle;
        PlaneCreator planecreator;
        FoundationCreator foundationCreator;
        Vector3 center;

        private void Awake()
        {

             _PlaneVectors.v01 = new Vector3(0,Height, 0);
            center = transform.position;
            foundationCreator = GetComponent<FoundationCreator>();
            planecreator = GetComponent<PlaneCreator>();
            if (planecreator != null ) 
            {
                SingleAngle = 360 / PlaneCount;
                mesh = GetComponent<MeshFilter>().mesh;
                UpdateMesh(StepX, StepY);
                Debug.Log(TriangleIndex);
                Debug.Log(VerticeIndex);
            }
        }


        void UpdateMesh(int X, int Y)
        {
            Triangels = new int[(((X + 1) * (Y + 1)) * 6) * PlaneCount+198];
            Vertices = new Vector3[((X+1) * (Y+1)) * PlaneCount+200];

            int[] both = new int[X * PlaneCount + 1];
            int[] top = new int[(X * PlaneCount + 1)];
            int iteration = 0;
            float CurrentAngle = 0;

            for(int i = 0; i < PlaneCount; i++)
            {
                _PlaneVectors = RecalculatePlaneVectors(_PlaneVectors, Radius, ref CurrentAngle);
                planecreator.CreatePlane(ref Triangels,ref Vertices, ref TriangleIndex, ref VerticeIndex, _PlaneVectors, X, Y, ref both, ref top, ref iteration);
                Debug.Log(i);
                
            }
            TriangleIndex = foundationCreator.CreateBoth(ref Triangels, TriangleIndex, ref VerticeIndex, both, Vertices);
            TriangleIndex = foundationCreator.CreateTop(ref Triangels, TriangleIndex, ref VerticeIndex, top, Vertices, Height);
            mesh.Clear();
            mesh.vertices = Vertices;
            mesh.triangles = Triangels;
            //Debug.Log(TriangleIndex);
           // Debug.Log(VerticeIndex);
        }

        PlaneVectors RecalculatePlaneVectors(PlaneVectors vectors, int radius,ref float CurrentAngale)
        {
            vectors.v00 = new Vector3( radius * geometry.cos(CurrentAngale - SingleAngle/2), vectors.v00.y, + radius * geometry.sin(CurrentAngale - SingleAngle/2));
            vectors.v01 = new Vector3(vectors.v00.x, vectors.v01.y, vectors.v00.z);
            vectors.v10 = new Vector3( radius * geometry.cos(CurrentAngale + SingleAngle/2), vectors.v00.y,  radius * geometry.sin(CurrentAngale + SingleAngle/2));
           vectors.v11 = new Vector3(vectors.v10.x, vectors.v01.y, vectors.v10.z);
           // Debug.Log(vectors.v00 + "  " + vectors.v10);

            CurrentAngale += SingleAngle;
            return vectors;
            //z = R * sin(угол)
            //x = R * cos(угол)
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            foreach (Vector3 i in Vertices)
            {
                Gizmos.DrawSphere(i + center, 0.25f);
            }
        }
    }
}
