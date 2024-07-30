using Intellectika.Geometry;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class MeshCreator : MonoBehaviour
    {

        [HideInInspector] public Mesh mesh;
        [HideInInspector] public int[] bottom;
        [HideInInspector] public int[] top;
        [HideInInspector] public int[] Triangels;
        [HideInInspector] public Vector3[] Vertices;

        [HideInInspector] public float SingleAngle = 0;
        [HideInInspector] public Vector3 center;
        [HideInInspector] public int TriangleIndex = 0;
        [HideInInspector] public int VerticeIndex = 0;
        [HideInInspector] public int iteration = 0;
        [HideInInspector] public float CurrentAngleX = 0;
        [HideInInspector] public float CurrentAngleY = 0;


        public int StepX = 2;
        public int StepY = 2;
        public int PlaneCount = 10;
        public int Radius = 5;
        public int Height = 4;

        public struct PlaneVectors
        {
            public Vector3 v00;
            public Vector3 v01;
            public Vector3 v10;
            public Vector3 v11;
        };

        public PlaneVectors planevectors;


        public virtual void ObjectInit()
        {
            planevectors = new PlaneVectors();
            mesh = GetComponent<MeshFilter>().mesh;
            SingleAngle = 360 / PlaneCount;
            center = transform.position;
            Triangels = new int[(StepX) * (StepY) * PlaneCount * 6 + (StepX) * (StepY) * 12 +99];
            Vertices = new Vector3[(StepX + 1)*(StepY + 1) * PlaneCount +3 +1000];
            bottom = new int[(StepX + 1) * (StepY + 1)];
            top = new int[(StepX + 1) * (StepY+1)];
            planevectors.v01 = new Vector3(0, Height, 0);
        }
        public virtual void Awake()
        {
                ObjectInit();
             //   UpdateMesh(StepX, StepY);
        }

        public void CreateBottom()/// создаёт треугольники для низа
        {
            int center = VerticeIndex + 1;
            Vertices[center] = Vector3.zero;
            VerticeIndex++;
            for (int i = 0; i < bottom.Length - 1; i++)
            {
                Triangels[TriangleIndex + 2] = bottom[i];
                Triangels[TriangleIndex + 1] = center;
                Triangels[TriangleIndex] = bottom[i + 1];
                TriangleIndex += 3;
                
            }
            

        }/// создаёт треугольники для низа

        public void CreateTop() ///создаёт треугольники для верха
        {
            int center = VerticeIndex + 1;
            Vertices[center] = new Vector3(0, Height, 0);
            VerticeIndex++;
            for (int i = 0; i < top.Length - 1; i++)
            {
                Triangels[TriangleIndex] = top[i];
                Triangels[TriangleIndex + 1] = center;
                Triangels[TriangleIndex + 2] = top[i + 1];
                TriangleIndex += 3;
            }

        }/// создаёт треугольники для верха

        public void CreatePlaneTriangels(int CurrentX) /// создаёт треугольники для Y сетки
        {
            int v00 = CurrentX + VerticeIndex;
            int v01 = CurrentX + VerticeIndex + StepX + 1;
            int v10 = CurrentX + VerticeIndex + 1;
            int v11 = CurrentX + 2 + VerticeIndex + StepX;
            Triangels[TriangleIndex] = v00;
            Triangels[TriangleIndex + 1] = Triangels[TriangleIndex + 4] = v01;
            Triangels[TriangleIndex + 2] = Triangels[TriangleIndex + 3] = v10;
            Triangels[TriangleIndex + 5] = v11;
            TriangleIndex += 6;
            } /// создаёт треугольники для Y сетки

        public void SetFoundationVertices(int currentY)  /// наполняет массивы верха и низа ID"шниками вершин
        {
            if (currentY == 0)
            {
                for (int x = 0; x <= StepX; x++)
                {
                    bottom[x + iteration] = x + VerticeIndex;
                }
                iteration += StepX;
            }

            if (currentY == StepY - 1)
            {
                for (int x = 0; x <= StepX; x++)
                {
                    top[x + iteration - StepX] = x + VerticeIndex + StepX +1;
                }

            }
        }/// наполняет массивы верха и низа ID"шниками вершин

        public PlaneVectors RecalculatePlaneVectors()
        {
            PlaneVectors vectors = planevectors; 
            vectors.v00 = new Vector3(Radius * geometry.cos(CurrentAngleX - SingleAngle / 2), vectors.v00.y, Radius * geometry.sin(CurrentAngleX - SingleAngle / 2));
            vectors.v01 = new Vector3(vectors.v00.x, vectors.v01.y, vectors.v00.z);
            vectors.v10 = new Vector3(Radius * geometry.cos(CurrentAngleX + SingleAngle / 2), vectors.v00.y, Radius * geometry.sin(CurrentAngleX + SingleAngle / 2));
            vectors.v11 = new Vector3(vectors.v10.x, vectors.v01.y, vectors.v10.z);

            CurrentAngleX += SingleAngle;
            return vectors;
        } /// перерасчитывает координаты начальных точек(PlaneVectors) в следующуие для плсторения плоских сторон


        public void SetPlaneSingle(int CurrentX, int CurrentY)
        {
            Vector3 v00 = new Vector3(Vector3.Lerp(planevectors.v00, planevectors.v10, (float)(CurrentX) / StepX).x,
            Vector3.Lerp(planevectors.v00, planevectors.v01, (float)(CurrentY) / StepY).y,
            Vector3.Lerp(planevectors.v00, planevectors.v10, (float)(CurrentX) / StepX).z);
            Vertices[CurrentX + VerticeIndex] = v00;
            Vector3 v01 = new Vector3(v00.x, Vector3.Lerp(planevectors.v00, planevectors.v01, (float)(CurrentY + 1) / StepY).y, v00.z);
            Vertices[CurrentX + VerticeIndex + StepX + 1] = v01;
            Vector3 v10 = new Vector3(Vector3.Lerp(planevectors.v00, planevectors.v10, (float)(CurrentX + 1) / StepX).x, Vector3.Lerp(planevectors.v00, planevectors.v01, (float)(CurrentY) / StepY).y,
                Vector3.Lerp(planevectors.v00, planevectors.v10, (float)(CurrentX + 1) / StepX).z);
            Vertices[CurrentX + VerticeIndex + 1] = v10;
            Vector3 v11 = new Vector3(v10.x, v01.y, v10.z);
            Vertices[CurrentX + VerticeIndex + StepX + 2] = v11;
        } // Устанавливает значения векторов вершинам в массиве вершин, по сути создаёт единичный квадрат для заполнения треугольниками

        //public PlaneVectors RecalculateSphereVectors()
        //{
        //    planevectors.v00 = new Vector3(Radius * geometry.cos(CurrentAngleX), 
        //                                    Radius * geometry.sin(CurrentAngleY), 
        //                                    Radius * geometry.sin(CurrentAngleX));


        //    planevectors.v01 = new Vector3(Radius * geometry.cos(CurrentAngleX),
        //                                Radius * geometry.sin(CurrentAngleY),
        //                                Radius * geometry.sin(CurrentAngleX));


        //    planevectors.v10 = new Vector3(Radius * geometry.cos(CurrentAngleX), 
        //                                    Radius * geometry.sin(CurrentAngleY), 
        //                                    Radius * geometry.sin(CurrentAngleX));


        //    planevectors.v11 = new Vector3(Radius * geometry.cos(CurrentAngleX), 
        //                                  Radius * geometry.sin(CurrentAngleY), 
        //                                 Radius * geometry.sin(CurrentAngleX));
        //    // Debug.Log(vectors.v00 + "  " + vectors.v10);

        //    CurrentAngleY += SingleAngle;
        //    CurrentAngleX += SingleAngle;
        //    return planevectors;
        //} /// перерасчитывает координаты начальных точек(PlaneVectors) в следующуие для плсторения сферы

        public PlaneVectors RecalculateSphereVectors()
        {
            PlaneVectors vectors = planevectors;
            vectors.v00 = new Vector3(Radius * geometry.cos(CurrentAngleX - SingleAngle / 2), vectors.v00.y, Radius * geometry.sin(CurrentAngleX - SingleAngle / 2));
            vectors.v01 = new Vector3(vectors.v00.x, vectors.v01.y, vectors.v00.z);
            vectors.v10 = new Vector3(Radius * geometry.cos(CurrentAngleX + SingleAngle / 2), vectors.v00.y, Radius * geometry.sin(CurrentAngleX + SingleAngle / 2));
            vectors.v11 = new Vector3(vectors.v10.x, vectors.v01.y, vectors.v10.z);

            vectors.v00 = vectors.v00.normalized;
            vectors.v01 = vectors.v01.normalized;
            vectors.v10 = vectors.v10.normalized;
            vectors.v11 = vectors.v11.normalized;

            CurrentAngleX += SingleAngle;
            return vectors;
        }
        public void CreatePlane()
        {

            for (int y = 0; y < StepY; y++)
            {
                for (int x = 0; x < StepX; x++)
                {
                    SetPlaneSingle(x , y);

                    CreatePlaneTriangels(x);

                }

                SetFoundationVertices(y);


                VerticeIndex += StepX + 1;
            }
            VerticeIndex += StepX + 1;
        } // создаёт плоскость

        //void UpdateMesh()
        //{


        //    for (int i = 0; i < PlaneCount; i++)
        //    {
        //        //_PlaneVectors = RecalculatePlaneVectors(_PlaneVectors, Radius, ref CurrentAngleX);
        //        // _PlaneVectors = RecalculateSphereVectors(_PlaneVectors, Radius, ref CurrentAngleY, ref CurrentAngleX);
        //        CreateSphere(ref Triangels, ref Vertices, ref TriangleIndex, ref VerticeIndex, _PlaneVectors, X, Y, ref both, ref top, ref iteration, Radius, ref CurrentAngleY, ref CurrentAngleX, SingleAngle);
        //        Debug.Log(i);

        //    }
        //    mesh.Clear();
        //    mesh.vertices = Vertices;
        //    mesh.triangles = Triangels;

        //}





        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.green;
        //    foreach (Vector3 i in Vertices)
        //    {
        //        Gizmos.DrawSphere(i + center, 0.25f);
        //    }
        //}





        //////////////////////////////////////////////////////////////////////////////////////////////////////
        ///










        public void CreateSphereBottom()
        {

            for (int y = 0; y < StepY; y++)
            {
                for (int x = 0; x < StepX; x++)
                {
                    SetSphereSingle(x, y);

                    CreatePlaneTriangels(x);

                }

                SetFoundationVertices(y);


                VerticeIndex += StepX + 1;
            }
            VerticeIndex += StepX + 1;

        }

        public void CreateSphereTop()
        {
            for (int y = StepY / 2; y < StepY; y++)
            {
                for (int x = 0; x < StepX; x++)
                {
                    planevectors = RecalculateSphereVectors();

                    SetPlaneSingle(x, y);

                }

                SetFoundationVertices(y);


                VerticeIndex += StepX + 1;
            }
            VerticeIndex += StepX + 1;
        }





        public void SetSphereSingle(int CurrentX, int CurrentY)
        {
            Vertices[CurrentX + VerticeIndex] = planevectors.v00;
            Vertices[CurrentX + VerticeIndex + StepX + 1] = planevectors.v01;
            Vertices[CurrentX + VerticeIndex + 1] = planevectors.v10;
            Vertices[CurrentX + VerticeIndex + StepX + 2] = planevectors.v11;
        } // Устанавливает значения векторов вершинам в массиве вершин, по сути создаёт единичный квадрат для заполнения треугольниками

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        public void CreateTopForSphere() ///создаёт треугольники для верха
        {
            int point = 0;
            int counter = 0;

                    for (int x = 0; x < StepX; x++)
                    {
                     point = VerticeIndex + 1;
                     Vertices[point] = new Vector3(Radius - counter - x -1 , Height, Radius - counter - z - 1);
                        VerticeIndex++;

                       Triangels[TriangleIndex] = top[counter];
                        Triangels[TriangleIndex + 1] = point;
                        Triangels[TriangleIndex + 2] = top[counter + 1];
                        TriangleIndex += 3;
                    }
                    counter++;

                for (int z = 1; z < StepX; z++)
                 {
                point = VerticeIndex + 1;
                Vertices[point] = new Vector3(Radius - counter - x - 1, Height, Radius - counter - z - 1);
                VerticeIndex++;

                Triangels[TriangleIndex] = top[counter];
                Triangels[TriangleIndex + 1] = point;
                Triangels[TriangleIndex + 2] = top[counter + 1];
                TriangleIndex += 3;
                }
                counter++;

                for (int x = StepX; x > 0; x--)
                {
                point = VerticeIndex + 1;
                Vertices[point] = new Vector3(Radius - counter - x - 1, Height, Radius - counter - z - 1);
                VerticeIndex++;

                Triangels[TriangleIndex] = top[counter];
                Triangels[TriangleIndex + 1] = point;
                Triangels[TriangleIndex + 2] = top[counter + 1];
                TriangleIndex += 3;
                }
                counter++;

                for (int x = 0; x < StepX; x++)
                {
                point = VerticeIndex + 1;
                Vertices[point] = new Vector3(Radius - counter - x - 1, Height, Radius - counter - z - 1);
                VerticeIndex++;

                Triangels[TriangleIndex] = top[counter];
                Triangels[TriangleIndex + 1] = point;
                Triangels[TriangleIndex + 2] = top[counter + 1];
                TriangleIndex += 3;
                }
                counter++;





        }


        public void SetPlaneSingleTop(int CurrentX, int CurrentZ)
        {
            planevectors.v10 = center - planevectors.v10;
              planevectors.v01 = center - planevectors.v01;
            planevectors.v11 = center - planevectors.v11;
            Vector3 v00 = new Vector3(Vector3.Lerp(planevectors.v00, planevectors.v10, (float)(CurrentX) / StepX).x,
            Height,
            Vector3.Lerp(planevectors.v00, planevectors.v10, (float)(CurrentZ) / StepX).z);
            Vertices[CurrentX + VerticeIndex] = v00;


            Vector3 v01 = new Vector3(v00.x, Height, Vector3.Lerp(planevectors.v00, planevectors.v01, (float)(CurrentZ+1) / StepX).z);
            Vertices[CurrentX + VerticeIndex + StepX + 1] = v01;


            Vector3 v10 = new Vector3(Vector3.Lerp(planevectors.v00, planevectors.v10, (float)(CurrentX + 1) / StepX).x, Height,
                v00.z);
            Vertices[CurrentX + VerticeIndex + 1] = v10;


            Vector3 v11 = new Vector3(v10.x, v01.y, v01.z);
            Vertices[CurrentX + VerticeIndex + StepX + 2] = v11;
        }
    }
}
