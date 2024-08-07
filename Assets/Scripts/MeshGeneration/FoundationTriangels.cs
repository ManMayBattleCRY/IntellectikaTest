using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    internal class FoundationTriangels // создаёт меши для верха и низа фигуры
    {
        Mesh Mesh;
        int Quality;
        Vector3 StartPoint;
        Vector3 EndPoint;
        Vector3 CenterPoint;

        bool Top = true;

        float Radius;

        internal Vector3[] Vertices;
        internal int[] Triangels;


        internal FoundationTriangels(Mesh _mesh, int _quality, Vector3 _startPoint, Vector3 _endPoint, Vector3 _centerPoint, float _Radius, bool _top)
        {
            Mesh = _mesh;
            Quality = _quality;
            StartPoint = _startPoint;
            EndPoint = _endPoint;
            CenterPoint = _centerPoint;
            Top = _top;
            Radius = _Radius;

            Vertices = new Vector3[(Quality + 1) * (Quality + 2)];
            Triangels = new int[(Quality + 1) * (Quality + 1) * 6];
        }

        internal void Create()
        {
            int i = 0;
            int TriangleIndex = 0;

            for (int x = 0; x < Quality; x++)
            {


                Vector3 v00 = new Vector3(Mathf.Lerp(StartPoint.x, EndPoint.x, (float)(x) / Quality),
                                          CenterPoint.y,
                                          Mathf.Lerp(StartPoint.z, EndPoint.z, (float)(x) / Quality));
                Vertices[x] = v00;


                Vector3 v10 = new Vector3(Mathf.Lerp(StartPoint.x, EndPoint.x, (float)(x + 1) / Quality),
                                          CenterPoint.y,
                                          Mathf.Lerp(StartPoint.z, EndPoint.z, (float)(x + 1) / Quality));
                Vertices[x + 1] = v10;

            }
            i += Quality + 1;

            for (int z = 0; z <= Quality; z++)
            {
                for (int x = 0; x < Quality; x++)
                {
                    Vector3 v00 = Vector3.Lerp(Vertices[x], CenterPoint, (float)(z) / Quality);
                    Vertices[x + i] = v00;


                    Vector3 v10 = Vector3.Lerp(Vertices[x + 1], CenterPoint, (float)(z) / Quality);
                    Vertices[x + i + 1] = v10;
                }
                i += Quality + 1;
            }
            i = 0;


            if (Top)
            {
                for (int z = 0; z <= Quality; z++)
                {
                    for (int x = 0; x < Quality; x++)
                    {
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
                    i += Quality + 1;
                }
            }
            else
            {
                for (int z = 0; z <= Quality; z++)
                {
                    for (int x = 0; x < Quality; x++)
                    {
                        int t00 = x + i;
                        int t01 = x + i + Quality + 1;
                        int t10 = x + i + 1;
                        int t11 = x + 2 + i + Quality;
                        Triangels[TriangleIndex + 5] = t00;
                        Triangels[TriangleIndex + 4] = t01;
                        Triangels[TriangleIndex + 3] = t10;
                        Triangels[TriangleIndex + 2] = t10;
                        Triangels[TriangleIndex + 1] = t01;
                        Triangels[TriangleIndex] = t11;
                        TriangleIndex += 6;
                    }
                    i += Quality + 1;
                }

            }

            Mesh.Clear();
            Mesh.vertices = Vertices;
            Mesh.triangles = Triangels;
        }
    }
}
