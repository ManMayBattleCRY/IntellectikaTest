using Intellectika.Geometry;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Intellectika
{
    public class Sphere : MeshCreatorv2
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
            planevectors = RecalculatePlaneVectors();
           // CreateLast();

            //  CreateBottom();
           //  CreateTopForSphere();
            //int counter = 0;
            //foreach (Vector3 i in Vertices)
            //{
            //    Vertices[counter] = i.normalized * Radius;
            //    counter++;
            //}
            mesh.Clear();
            mesh.vertices = Vertices;
            mesh.triangles = Triangels;
          //  mesh.RecalculateNormals();
            



        }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            foreach (Vector3 i in Vertices)
            {
                Gizmos.DrawSphere(i, 0.25f);
            }

            //foreach (int i in top)
            //{
            //    Gizmos.DrawSphere(Vertices[i], 0.25f);
            //}

            //foreach (int i in bottom)
            //{
            //    Gizmos.DrawSphere(Vertices[i], 0.25f);
            //}

            //Gizmos.DrawSphere(Vertices[top[0]], 0.25f);

            //Gizmos.DrawSphere(Vertices[top[StepX]], 0.25f);
        }
    }
}
