using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    public abstract class TestPlane : MonoBehaviour
    {

        enum PlaneFace
        {
            forward, backward,
            right, left,
            down, up
        }
        public struct PlaneVertices
        {
            Vector3 A, B, C, D;

            public Vector3 GetVertice(int number)
            {
                switch (number)
                {
                    case 1: return A;
                    case 2: return B;
                    case 3: return C;
                    case 4: return D;
                    default: return Vector3.zero;
                }
            }

            public void Set(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
            {
                A = a; B = b; C = c; D = d;
            }
        }

        public class PlaneTriangels
        {
            PlaneTriangels()
            {
                InitTriangels();
            }
            int[] t1;
            int[] t2;

            public int[] Get(bool TriangelNumber)
            {
                if (TriangelNumber) return t2;
                else return t1;
            }


            void InitTriangels()
            {
                t1 = new int[3];
                t2 = new int[3];
            }
        }

         public abstract PlaneTriangels triangles();
         public abstract PlaneVertices vertices();
        
     }
}
