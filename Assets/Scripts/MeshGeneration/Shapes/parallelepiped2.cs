using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace Intellectika
//{
//    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
//    public class parallelepiped2 : MonoBehaviour
//    {

//        Vector3[] Vertices =
//            {
//            new Vector3(0,0,0),
//            new Vector3(1,0,0),
//            new Vector3(1,0,1),
//            new Vector3(0,0,1),
//            new Vector3(0,1,0),
//            new Vector3(1,1,0),
//            new Vector3(1,1,1),
//            new Vector3(0,1,1),
//            };
//        int[] Triangles = new int[8*6];

//        int[] CreateTriangle()
//        {

//            int vIndex = 0;
//            int tIndex = 0;
//            for (int y = 0; y <= 1; y++)
//            {

//                for (int x = 0; x < 3; x++)
//                {
//                    Triangles[tIndex] = vIndex;
//                    Triangles[tIndex + 1] = Triangles[tIndex + 4] = 1 + vIndex + 1;
//                    Triangles[tIndex + 2] = Triangles[tIndex + 3] = vIndex + 1;
//                    Triangles[tIndex + 5] = vIndex + 1 + 2;


//                    vIndex++;
//                    tIndex += 6;
//                }
//                vIndex++;

//            }

//            return Triangles;

//        }

//        void CreateParallelepiped()
//        { 
//            Triangles = CreateTriangle();
//            Mesh mesh = GetComponent<MeshFilter>().mesh;
//            mesh.triangles = Triangles;
//            mesh.vertices = Vertices;
//        }

//        private void Awake()
//        {
//            CreateParallelepiped();
//        }
//    }
//}
