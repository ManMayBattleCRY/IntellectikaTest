using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    internal class PlanePointsCreator
    {
        Mesh Mesh;
        int Resolution;
        Vector3 up;
        Vector3 AxisA;
        Vector3 AxisB;
        float offset;
        

        internal Vector3[] Vertices;
        internal int[] triangels;

        internal PlanePointsCreator(Mesh _mesh, int resolution, Vector3 _up, float _offset)
        {
            Mesh = _mesh;
            Resolution = resolution;
            up = _up;
            offset = _offset;
   
        }

        internal void Create()
        {

            AxisA = new Vector3(up.y, up.z, up.x); // axis A = x if up = Vector3.up
            AxisB = Vector3.Cross(up, AxisA); // axis B = z if up = Vector3.up

            Vertices = new Vector3[Resolution * Resolution];
            triangels = new int[(Resolution - 1) * (Resolution - 1) * 6];
            int TriangelIndex = 0;

            int i = 0;

            for (int x = 0; x < Resolution; x++)
            {
                for (int y = 0; y < Resolution; y++)
                {
                    i = x + y*Resolution; 
                    Vector2 percent = new Vector2(x, y) / (Resolution - 1);
                    Vector3 PointOnUnitCube = up + (percent.x - 0.5f) * 2 * AxisA + (percent.y - 0.5f) * 2 * AxisB;
                    Vertices[i] = PointOnUnitCube;

                    if (x != Resolution - 1 && y != Resolution - 1)
                    {
                        triangels[TriangelIndex] = triangels[TriangelIndex + 3] = i;
                        triangels[TriangelIndex + 1] = triangels[TriangelIndex + 5] = i + Resolution + 1;
                        triangels[TriangelIndex + 4] = i + 1;
                        triangels[TriangelIndex + 2] = i + Resolution;
                        TriangelIndex += 6;
                    }
                    
                }
            }

            Mesh.Clear();
            Mesh.vertices = Vertices;
            Mesh.triangles = triangels;

        }



    }
}
