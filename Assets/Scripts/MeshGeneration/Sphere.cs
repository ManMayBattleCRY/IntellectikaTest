using Intellectika.Geometry;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Intellectika
{
    public class Sphere : MeshCreator
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
            //  CreateBottom();
            CreateTopForSphere();
            int counter = 0;
            //foreach (Vector3 i in Vertices)
            //{
            //    Vertices[counter] = i.normalized * Radius;
            //    counter++;
            //}
            mesh.Clear();
            mesh.vertices = Vertices;
            mesh.triangles = Triangels;




        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            foreach (Vector3 i in Vertices)
            {
                Gizmos.DrawSphere(i, 0.25f);
            }
        }
    }
}
