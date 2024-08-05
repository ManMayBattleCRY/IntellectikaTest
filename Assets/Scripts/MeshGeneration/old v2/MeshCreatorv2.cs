using Intellectika.Geometry;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace Intellectika
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class MeshCreatorv2 : MonoBehaviour
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
            Triangels = new int[(StepX) * (StepY) * PlaneCount * 6 + (StepX) * (StepY) * 12 * PlaneCount];
            Vertices = new Vector3[(StepX + 1) * (StepY + 1) * PlaneCount + 3 + 1000];
            bottom = new int[(StepX + 1) * (StepY + 1) * PlaneCount * PlaneCount];
            top = new int[(StepX + 1) * (StepY + 1) * PlaneCount * PlaneCount];
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



        //public void CreateLast()
        //{
        //    for (int y = 0; y < StepY; y++)
        //    {
        //        for (int x = 0; x < StepX - 1; x++)
        //        {
        //            SetPlaneSingle(x, y);

        //            CreatePlaneTriangels(x);

        //        }

        //        SetFoundationVertices(y);


        //        VerticeIndex += StepX + 1;
        //    }
        //    VerticeIndex += StepX + 1;


        //    for (int y = 0; y < StepY; y++)
        //    {
        //        //Vector3 v00 = new Vector3(Vector3.Lerp(planevectors.v00, planevectors.v10, (float)(StepX) / StepX).x,
        //        //Vector3.Lerp(planevectors.v00, planevectors.v01, (float)(y) / StepY).y,
        //        //Vector3.Lerp(planevectors.v00, planevectors.v10, (float)(StepX) / StepX).z);
        //        //Vertices[StepX + VerticeIndex] = v00;


        //        //Vector3 v01 = new Vector3(v00.x, Vector3.Lerp(planevectors.v00, planevectors.v01, (float)(y + 1) / StepY).y, v00.z);
        //        //Vertices[StepX + VerticeIndex + StepX + 1] = v01;


        //        //Vector3 v10 = Vertices[(StepX + 1) * y];
        //        //Vertices[StepX + VerticeIndex + 1] = v10;


        //        //Vector3 v11 = Vertices[(StepX + 1) * y + (StepX + 1)];
        //        //Vertices[StepX + VerticeIndex + StepX + 2] = v11;

        //      //  SetFoundationVertices(y);
        //        //CreatePlaneTriangels(StepX + 1);  


        //        int v00 =  VerticeIndex - ((StepX + 1) * (StepY + 1) + StepX - 1);
        //        int v01 =  (StepX + 1) * y;
        //        int v10 = VerticeIndex - ((StepX) * (StepY)) + StepX + 1 ;
        //        int v11 = (StepX + 1) * y + (StepX + 1);
        //        Triangels[TriangleIndex] = v00;
        //        Triangels[TriangleIndex + 1] = Triangels[TriangleIndex + 4] = v01;
        //        Triangels[TriangleIndex + 2] = Triangels[TriangleIndex + 3] = v10;
        //        Triangels[TriangleIndex + 5] = v11;
        //        TriangleIndex += 6;

        //    }



        //}


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


            CurrentAngleX += SingleAngle;
            return vectors;
        }
        public void CreatePlane()
        {




            for (int y = 0; y < StepY; y++)
            {
                for (int x = 0; x < StepX; x++)
                {
                    SetPlaneSingle(x, y);

                    CreatePlaneTriangels(x);

                }

                SetFoundationVertices(y);


                VerticeIndex += StepX+ 1 ;
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

        public void CreateTopForSphere1() ///создаёт треугольники для верха
        {
            int[] indexes = new int[999];
            planevectors.v00 = Vertices[top[0]];
            planevectors.v10 = Vertices[top[StepX]];
            planevectors.v01 = Vertices[top[StepX * 3]];
            planevectors.v11 = new Vector3(planevectors.v01.x, Height, planevectors.v10.z);
            for (int z = 0; z < StepX  ; z++)
            {
                for (int x = 0; x < StepX  ; x++)
                {
                    Vector3 v00 = new Vector3(Vector3.Lerp(planevectors.v00, planevectors.v01, (float)(x) / StepX).x,
                    Height,
                    Vector3.Lerp(planevectors.v00, planevectors.v10, (float)(z) / StepX).z);
                    Vertices[x + VerticeIndex] = v00;
                    Vector3 v01 = new Vector3(Vector3.Lerp(planevectors.v00, planevectors.v01, (float)(x + 1) / StepX).x, Height, v00.z);
                    Vertices[x + VerticeIndex + 1] = v01;
                    Vector3 v10 = new Vector3(Vector3.Lerp(planevectors.v00, planevectors.v01, (float)(x) / StepX).x, Height,
                        Vector3.Lerp(planevectors.v00, planevectors.v10, (float)(z + 1) / StepX).z);
                    Vertices[x + VerticeIndex + StepX + 1] = v10;
                    Vector3 v11 = new Vector3(Vector3.Lerp(planevectors.v00, planevectors.v01, (float)(x + 1) / StepX).x, 
                        Height, 
                        Vector3.Lerp(planevectors.v00, planevectors.v10, (float)(z + 1) / StepX).z);
                    Vertices[x + VerticeIndex + StepX + 2] = v11;


                    int vv00 = x + VerticeIndex;
                    int vv01 = x + VerticeIndex + 1;
                    int vv10 = x + VerticeIndex + StepX + 1;
                    int vv11 = x + VerticeIndex + StepX + 2;
                    Triangels[TriangleIndex] = vv00;
                    Triangels[TriangleIndex + 1] = vv01;
                    Triangels[TriangleIndex + 2] = vv11;
                    Triangels[TriangleIndex + 3] = vv11;
                    Triangels[TriangleIndex + 4] = vv10;
                    Triangels[TriangleIndex + 5] = vv00;

                    TriangleIndex += 6;

                }
                VerticeIndex += StepX + 1;
            }


        }


        public void CreateTopForSphere2() ///создаёт треугольники для верха
        {
            int[] indexes = new int[999];
            planevectors.v00 = Vertices[top[0]];
            planevectors.v10 = Vertices[top[StepX]];
            planevectors.v01 = Vertices[top[StepX * 3]];
            planevectors.v11 = new Vector3(planevectors.v01.x, Height, planevectors.v10.z);
            float plusradius = (float)Radius / (float)(PlaneCount);
            float currentradius = 0;

            for (int z = 0; z < PlaneCount; z++)
            {
                for (int x = 0; x < StepX * PlaneCount; x++)
                {

                    CurrentAngleX += SingleAngle;

                    Vertices[x + VerticeIndex] = planevectors.v00;
                    Vertices[x + VerticeIndex + StepX + 1] = planevectors.v01;
                    Vertices[x + VerticeIndex + 1] = planevectors.v10;
                    Vertices[x + VerticeIndex + StepX + 2] = planevectors.v11;


                    int vv00 = x + VerticeIndex;
                    int vv01 = x + VerticeIndex + StepX + 1;
                    int vv10 = x + VerticeIndex + 1;
                    int vv11 = x + VerticeIndex + StepX + 2;
                    Triangels[TriangleIndex] = vv00;
                    Triangels[TriangleIndex + 1] = vv01;
                    Triangels[TriangleIndex + 2] = vv11;
                    Triangels[TriangleIndex + 3] = vv11;
                    Triangels[TriangleIndex + 4] = vv10;
                    Triangels[TriangleIndex + 5] = vv00;


                    planevectors.v00 = new Vector3((Radius - currentradius) * geometry.cos(CurrentAngleX - SingleAngle / 2), planevectors.v00.y, (Radius - currentradius) * geometry.sin(CurrentAngleX - SingleAngle / 2));
                    planevectors.v01 = new Vector3(planevectors.v00.x, planevectors.v01.y, planevectors.v00.z);
                    planevectors.v10 = new Vector3((Radius - currentradius) * geometry.cos(CurrentAngleX + SingleAngle / 2), planevectors.v00.y, (Radius - currentradius) * geometry.sin(CurrentAngleX + SingleAngle / 2));
                    planevectors.v11 = new Vector3(planevectors.v10.x, planevectors.v01.y, planevectors.v10.z);




                    TriangleIndex += 6;

                }
                currentradius += plusradius;
                VerticeIndex += StepX + 1;

            }



        }



        public void CreateTopForSphere() ///создаёт треугольники для верха
        {

            float plusradius = (float)Radius / (float)(PlaneCount);
            float currentradius = plusradius *2;
            planevectors.v10 = Vertices[top[StepX]];
            planevectors.v11 = new Vector3(Vertices[top[StepX * 3]].x, Height, Vertices[top[StepX]].z);

            float SingleAngleForSphere = (float)(360/(StepX * PlaneCount) + 1);

            for (int i = 0; i < PlaneCount - 1; i++)
            {
                for (int x = 0; x < StepX ; x++)
                {

                    planevectors.v00 = Vertices[top[x + i * StepX]];
                    planevectors.v01 = Vertices[top[x + i * StepX + 1]];

                    planevectors.v10 = new Vector3((Radius - currentradius) * geometry.cos(CurrentAngleX - SingleAngle / 2), planevectors.v10.y, (Radius - currentradius) * geometry.sin(CurrentAngleX - SingleAngle / 2));
                    planevectors.v11 = new Vector3((Radius - currentradius) * geometry.cos(CurrentAngleX + SingleAngle / 2), planevectors.v10.y, (Radius - currentradius) * geometry.sin(CurrentAngleX + SingleAngle / 2));

                    CurrentAngleX += SingleAngleForSphere;

                    Vertices[x + VerticeIndex + 1] = planevectors.v10;
                    Vertices[x + VerticeIndex + 2] = planevectors.v11;

                    int vv00 = top[x + i * StepX];
                    int vv01 = top[x + i * StepX + 1];
                    int vv10 = x + VerticeIndex;
                    int vv11 = x + VerticeIndex + 1;
                    Triangels[TriangleIndex] = vv00;
                    Triangels[TriangleIndex + 1] = Triangels[TriangleIndex + 4] = vv10;
                    Triangels[TriangleIndex + 2] = Triangels[TriangleIndex + 3] = vv01;
                    Triangels[TriangleIndex + 5] = vv11;
                    TriangleIndex += 6;




                }
                VerticeIndex += StepX;
            }

                
                

            



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


            Vector3 v01 = new Vector3(v00.x, Height, Vector3.Lerp(planevectors.v00, planevectors.v01, (float)(CurrentZ + 1) / StepX).z);
            Vertices[CurrentX + VerticeIndex + StepX + 1] = v01;


            Vector3 v10 = new Vector3(Vector3.Lerp(planevectors.v00, planevectors.v10, (float)(CurrentX + 1) / StepX).x, Height,
                v00.z);
            Vertices[CurrentX + VerticeIndex + 1] = v10;


            Vector3 v11 = new Vector3(v10.x, v01.y, v01.z);
            Vertices[CurrentX + VerticeIndex + StepX + 2] = v11;
        }
    }
  
}
