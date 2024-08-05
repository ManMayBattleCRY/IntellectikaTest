using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    public class Cube : MeshCreatorv2

    {

        private void Start()
        {
               UpdateMesh();
        }
        void UpdateMesh()
        {


            for (int i = 0; i < PlaneCount; i++)
            {
                planevectors = RecalculatePlaneVectors();

                CreatePlane();
            }
            CreateBottom();
            CreateTop();
            mesh.Clear();
            mesh.vertices = Vertices;
            mesh.triangles = Triangels;

        }
    }
}
