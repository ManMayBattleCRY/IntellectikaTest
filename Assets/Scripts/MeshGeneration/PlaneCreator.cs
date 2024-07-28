using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    public class PlaneCreator : MeshData
        
    {


        public void CreatePlane(ref int[] triangels,ref Vector3[] vertices,ref int triangleindex,ref int verticeindex, PlaneVectors planeVectors, int X, int Y,ref int[] both,ref int[] top,ref int iteration)
        {

            Vector3 v00 = Vector3.zero;
            Vector3 v01 = Vector3.zero;
            Vector3 v10 = Vector3.zero;
            Vector3 v11 = Vector3.zero;


            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    v00 = new Vector3(Vector3.Lerp(planeVectors.v00, planeVectors.v10, (float)(x) / X).x ,
                        Vector3.Lerp(planeVectors.v00, planeVectors.v01, (float)(y) / Y).y ,
                        Vector3.Lerp(planeVectors.v00, planeVectors.v10, (float)(x) / X).z );
                    vertices[x + verticeindex] = v00; 
                    v01 = new Vector3(v00.x, Vector3.Lerp(planeVectors.v00, planeVectors.v01, (float)(y + 1) / Y).y , v00.z);
                    vertices[x + verticeindex + X + 1] = v01; 
                    v10 = new Vector3(Vector3.Lerp(planeVectors.v00, planeVectors.v10, (float)(x + 1) / X).x , Vector3.Lerp(planeVectors.v00, planeVectors.v01, (float)(y) / Y).y ,
                        Vector3.Lerp(planeVectors.v00, planeVectors.v10, (float)(x + 1) / X).z );
                    vertices[x + verticeindex + 1] = v10; 
                    v11 = new Vector3(v10.x, v01.y, v10.z);
                    vertices[x + verticeindex + X + 2] = v11;

                    triangleindex = SetPlane(triangels, triangleindex, x + verticeindex, x + verticeindex + X + 1, x + verticeindex + 1, x + 2 + verticeindex + X);

                }


                if (y == 0)
                {
                    for (int x = 0; x <= X; x++)
                    {
                        both[x + iteration] = x + verticeindex;
                    }
                    iteration += X;
                }

                if (y == Y - 1)
                {
                    for (int x = 0; x <= X; x++)
                    {
                        top[x + iteration -X] = x + verticeindex + X + 1;
                    }
                    
                }

                verticeindex += X + 1;
            }
            //  return verticeindex;
           verticeindex += X + 1;

            

        }

        public int SetPlane(int[] triangles, int tIndex, int v00, int v01, int v10, int v11)
        {

            triangles[tIndex] = v00;
            triangles[tIndex + 1] = triangles[tIndex + 4] = v01;
            triangles[tIndex + 2] = triangles[tIndex + 3] = v10;
            triangles[tIndex + 5] = v11; 
            return tIndex + 6;
        }

    }
}
