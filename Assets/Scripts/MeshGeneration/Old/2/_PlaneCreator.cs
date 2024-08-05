using Intellectika.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Intellectika
{
    public class _PlaneCreator : MonoBehaviour

    {

        public struct PlaneVectors
        {
            public Vector3 v00;
            public Vector3 v01;
            public Vector3 v10;
            public Vector3 v11;
        };
        public PlaneVectors _PlaneVectors;

        public void CreatePlane(ref int[] triangels, ref Vector3[] vertices, ref int triangleindex, ref int verticeindex, PlaneVectors planeVectors, int X, int Y, ref int[] bottom, ref int[] top, ref int iteration)
        {
            Vector3 v00 = Vector3.zero;
            Vector3 v01 = Vector3.zero;
            Vector3 v10 = Vector3.zero;
            Vector3 v11 = Vector3.zero;



            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    CreatePlaneGrid(ref vertices, ref verticeindex, planeVectors, X, Y, x, y);

                    triangleindex = SetPlane(triangels, triangleindex, x + verticeindex, x + verticeindex + X + 1, x + verticeindex + 1, x + 2 + verticeindex + X);

                }

                CreateFoundationVertices(y, ref verticeindex, X, Y, ref bottom, ref top, ref iteration);


                verticeindex += X + 1;
            }
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

        void CreateFoundationVertices(int currentY, ref int verticeindex, int X, int Y, ref int[] bottom, ref int[] top, ref int iteration)
        {
            if (currentY == 0)
            {
                for (int x = 0; x <= X; x++)
                {
                    bottom[x + iteration] = x + verticeindex;
                }
                iteration += X;
            }

            if (currentY == Y - 1)
            {
                for (int x = 0; x <= X; x++)
                {
                    top[x + iteration - X] = x + verticeindex + X + 1;
                }

            }
        }
        void CreatePlaneGrid(ref Vector3[] vertices, ref int verticeindex, PlaneVectors planeVectors, int X, int Y, int CurrentX, int CurrentY)
        {
            Vector3 v00 = new Vector3(Vector3.Lerp(planeVectors.v00, planeVectors.v10, (float)(CurrentX) / X).x,
            Vector3.Lerp(planeVectors.v00, planeVectors.v01, (float)(CurrentY) / Y).y,
            Vector3.Lerp(planeVectors.v00, planeVectors.v10, (float)(CurrentX) / X).z);
            vertices[CurrentX + verticeindex] = v00;
            Vector3 v01 = new Vector3(v00.x, Vector3.Lerp(planeVectors.v00, planeVectors.v01, (float)(CurrentY + 1) / Y).y, v00.z);
            vertices[CurrentX + verticeindex + X + 1] = v01;
            Vector3 v10 = new Vector3(Vector3.Lerp(planeVectors.v00, planeVectors.v10, (float)(CurrentX + 1) / X).x, Vector3.Lerp(planeVectors.v00, planeVectors.v01, (float)(CurrentY) / Y).y,
                Vector3.Lerp(planeVectors.v00, planeVectors.v10, (float)(CurrentX + 1) / X).z);
            vertices[CurrentX + verticeindex + 1] = v10;
            Vector3 v11 = new Vector3(v10.x, v01.y, v10.z);
            vertices[CurrentX + verticeindex + X + 2] = v11;
        }


        public void CreateSphere(ref int[] triangels, ref Vector3[] vertices, ref int triangleindex, ref int verticeindex, PlaneVectors planeVectors, int X, int Y, ref int[] bottom, ref int[] top, ref int iteration,int Radius, ref float CurrentAngleY, ref float CurrentAngleX, float SingleAngle)
        {
            Vector3 v00 = Vector3.zero;
            Vector3 v01 = Vector3.zero;
            Vector3 v10 = Vector3.zero;
            Vector3 v11 = Vector3.zero;



            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                   planeVectors = RecalculateSphereVectors(planeVectors, Radius,ref CurrentAngleY,ref CurrentAngleX, SingleAngle);

                    triangleindex = SetPlane(triangels, triangleindex, x + verticeindex, x + verticeindex + X + 1, x + verticeindex + 1, x + 2 + verticeindex + X);

                }

                CreateFoundationVertices(y, ref verticeindex, X, Y, ref bottom, ref top, ref iteration);


                verticeindex += X + 1;
            }
            verticeindex += X + 1;
        }

        PlaneVectors RecalculateSphereVectors(PlaneVectors vectors, int radius, ref float currentangaleY, ref float currentangaleX ,float SingleAngle)
        {
            vectors.v00 = new Vector3(radius * geometry.cos(currentangaleX - SingleAngle / 2), radius * geometry.sin(currentangaleX - SingleAngle / 2), radius * geometry.sin(currentangaleX - SingleAngle / 2));
            vectors.v01 = new Vector3(radius * geometry.cos(currentangaleX - SingleAngle / 2),
                                        radius * geometry.sin(currentangaleX - SingleAngle),
                                        radius * geometry.sin(currentangaleX - SingleAngle));
            vectors.v10 = new Vector3(radius * geometry.cos(currentangaleX + SingleAngle / 2), radius * geometry.sin(currentangaleX + SingleAngle / 2), radius * geometry.sin(currentangaleX + SingleAngle / 2));
            vectors.v11 = new Vector3(radius * geometry.cos(currentangaleX + SingleAngle / 2), radius * geometry.sin(currentangaleX + SingleAngle), radius * geometry.sin(currentangaleX + SingleAngle / 2));
            // Debug.Log(vectors.v00 + "  " + vectors.v10);

            currentangaleY += SingleAngle;
            currentangaleX += SingleAngle;
            return vectors;
            //z = R * sin(угол)
            //x = R * cos(угол)
        }
    }


}
