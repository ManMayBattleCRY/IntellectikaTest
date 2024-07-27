using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Intellectika
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class PlaneCreator : MonoBehaviour
    {
        Mesh mesh;
        [Serializable]
        struct PlaneVectors
        {
            public Vector3 v00;
            public Vector3 v01;
            public Vector3 v10;
            public Vector3 v11;
        };
        public int StepX = 6;
        public int StepY = 6;
        int TriangleIndex = 0;
        int VerticeIndex = 0;
        int[] Triangels;
        Vector3[] Vertices;

        [SerializeField] PlaneVectors _PlaneVectors;

        private void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;
            UpdateMesh(StepX,StepY);
        }



        int CreateGrid(int[] triangels, PlaneVectors planeVectors, int X, int Y)
        {
           // Debug.Log(Vertices.Length);
           Vector3 v00 = Vector3.zero;
           Vector3 v01 = Vector3.zero;
           Vector3 v10 = Vector3.zero;
           Vector3 v11 = Vector3.zero;
      //      X = X + Convert.ToInt32(_PlaneVectors.v10.x) / X ;
       //     Y = Y + Convert.ToInt32(_PlaneVectors.v01.y) / Y;
            Debug.Log(X);
            int i = 0;
            for (int y = 0; y < Y ; y++)
            {
                for (int x = 0; x < X ; x++)
                {
                    v00 = new Vector3(Vector3.Lerp(planeVectors.v00, planeVectors.v10, (float)(x) / X).x, Vector3.Lerp(planeVectors.v00, planeVectors.v01, (float)(y) / Y).y, v00.z);
                    Vertices[x+ VerticeIndex] = v00; i++;
                    v01 = new Vector3(v00.x, Vector3.Lerp(planeVectors.v00, planeVectors.v01, (float)(y + 1) / Y).y, v00.z);
                    Vertices[x + VerticeIndex + X + 1] = v01; i++;
                    v10 = new Vector3(Vector3.Lerp(planeVectors.v00, planeVectors.v10, (float)(x + 1) / X).x, Vector3.Lerp(planeVectors.v00, planeVectors.v01, (float)(y) / Y).y, v00.z);
                    Vertices[x+ VerticeIndex + 1] = v10; i++;
                    v11 = new Vector3(v10.x, v01.y, v00.z);
                    Vertices[x + VerticeIndex + X + 2] = v11; i++;

                    TriangleIndex = SetPlane(triangels, TriangleIndex,x + VerticeIndex,x + VerticeIndex + X + 1, x + VerticeIndex + 1,x + 2 + VerticeIndex + X);
                    


                    
                }
                VerticeIndex += StepX + 1;
            }

            //for (int x = 0; x < X ; x++)
            //{
            //    v00 = new Vector3(Vector3.Lerp(planeVectors.v00, planeVectors.v10, (float)(x) / X).x, Vector3.Lerp(planeVectors.v00, planeVectors.v01, (float)(StepY-1) / Y).y, v00.z);
            //    Vertices[x + VerticeIndex] = v00; i++;
            //    v01 = new Vector3(v00.x, Vector3.Lerp(planeVectors.v00, planeVectors.v01, (float)(StepY) / Y).y, v00.z);
            //    Vertices[x + VerticeIndex + X + 1] = v01; i++;
            //    v10 = new Vector3(Vector3.Lerp(planeVectors.v00, planeVectors.v10, (float)(x + 1) / X).x, Vector3.Lerp(planeVectors.v00, planeVectors.v01, (float)(StepY-1) / Y).y, v00.z);
            //    Vertices[x + VerticeIndex + 1] = v10; i++;
            //    v11 = new Vector3(v10.x, v01.y, v00.z);
            //    Vertices[x + VerticeIndex + X + 2] = v11; i++;

            //    TriangleIndex = SetPlane(triangels, TriangleIndex, x + VerticeIndex, x + VerticeIndex + X, x + VerticeIndex + 1, x + 1 + VerticeIndex + X);
            //}
            return VerticeIndex;
        }

        int SetPlane(int[] triangles, int tIndex, int v00, int v01, int v10, int v11)
        {

            triangles[tIndex] = v00;
            triangles[tIndex + 1] = triangles[tIndex + 4] = v01;
            triangles[tIndex + 2] = triangles[tIndex + 3] = v10;
            triangles[tIndex + 5] = v11; Debug.Log(tIndex);
            return tIndex + 6;
        }

        void UpdateMesh(int X, int Y)
        {
            //Triangels = new int[((X + 1) * (Y + 1)) * 6];
            //Vertices = new Vector3[((X+1) * (Y+1))];
            Triangels = new int[333];
            Vertices = new Vector3[333];


            mesh.Clear();
            CreateGrid(Triangels, _PlaneVectors, X, Y);
            mesh.vertices = Vertices;
            mesh.triangles = Triangels;


        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (Vector3 v in Vertices)
            {
                Gizmos.DrawSphere(v, 0.25f);
            }
        }
    }
}
