using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

namespace Intellectika
{
    internal class PlaneCreator
    {
        Mesh Mesh;
        int Quality;
        public Vector3 StartPoint;
        public Vector3 EndPoint;


        internal Vector3[] Vertices;
        internal int[] Triangels;

        

        internal PlaneCreator(Mesh mesh, int quality, Vector3 _startPoint, Vector3 _endPoint)
        {
            Mesh = mesh;
            Quality = quality;

            StartPoint = _startPoint;
            EndPoint = _endPoint;

            

            Vertices = new Vector3[(Quality+1) * (Quality+1)];
            Triangels = new int[(Quality + 1) * (Quality + 1) * 6];
        }

        internal void Create()
        {
            int i = 0;
            int TriangleIndex = 0;
            for (int y = 0; y < Quality; y++)
            {
                for (int x = 0; x < Quality; x++)
                {


                    Vector3 v00 = new Vector3(Mathf.Lerp(StartPoint.x, EndPoint.x, (float)(x) / Quality),
                                              Mathf.Lerp(StartPoint.y, EndPoint.y, (float)(y) / Quality),
                                              Mathf.Lerp(StartPoint.z, EndPoint.z, (float)(x) / Quality));
                    Vertices[x + i] = v00;


                    Vector3 v01 = new Vector3(v00.x,
                                              Mathf.Lerp(StartPoint.y, EndPoint.y, (float)(y + 1) / Quality ),
                                              v00.z);
                    Vertices[x + i + Quality + 1] = v01;


                    Vector3 v10 = new Vector3(Mathf.Lerp(StartPoint.x, EndPoint.x, (float)(x + 1) / Quality),
                                              Mathf.Lerp(StartPoint.y, EndPoint.y, (float)(y) / Quality),
                                              Mathf.Lerp(StartPoint.z, EndPoint.z, (float)(x + 1) / Quality));
                    Vertices[x + i + 1] = v10;


                    Vector3 v11 = new Vector3(v10.x, v01.y, v10.z);
                    Vertices[x +i + Quality + 2] = v11;


                    int t00 = x + i;
                    int t01 = x + i + Quality + 1;
                    int t10 = x + i + 1;
                    int t11 = x + 2 + i + Quality;
                    Triangels[TriangleIndex] = t00;
                    Triangels[TriangleIndex + 1] = Triangels[TriangleIndex + 4] = t01;
                    Triangels[TriangleIndex + 2] = Triangels[TriangleIndex + 3] = t10;
                    Triangels[TriangleIndex + 5] = t11;
                    TriangleIndex += 6;

                    
                }
                i+= Quality + 1;
            }

            Mesh.Clear();
            Mesh.vertices = Vertices;
            Mesh.triangles = Triangels;
        }

    }

}
