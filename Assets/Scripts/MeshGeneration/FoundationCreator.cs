using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    public class FoundationCreator : MeshData
    {

        public int CreateBoth(ref int[] triangles, int tIndex, ref int vIndex, int[] HighVertices, Vector3[] Vertices)
        {
            int center = vIndex + 1;
            Vertices[center] = Vector3.zero;
            vIndex++;
            for (int i = 0; i < HighVertices.Length - 1; i++)
            {
                triangles[tIndex + 2] = HighVertices[i];
                triangles[tIndex + 1] = center;
                triangles[tIndex] = HighVertices[i + 1];
                tIndex += 3;
            }
            return tIndex;

        }

        public int CreateTop(ref int[] triangles, int tIndex, ref int vIndex, int[] HighVertices, Vector3[] Vertices, int height)
        { 
            int center = vIndex + 1;
            Vertices[center] = new Vector3( 0 , height, 0);
            vIndex++;
            for (int i = 0; i < HighVertices.Length - 1; i++)
            {
                triangles[tIndex] = HighVertices[i];
                triangles[tIndex + 1] = center;
                triangles[tIndex + 2] = HighVertices[i + 1];
                tIndex += 3;
            }
            return tIndex;

        }



    }
}
