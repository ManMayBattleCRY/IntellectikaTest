using Palmmedia.ReportGenerator.Core.Parser.Filtering;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Intellectika
{
    internal class Capsule : MeshObjects
    {
        [SerializeField] private protected float Height = 4;
        private void Awake()
        {
            Init();
        }

        private protected override void Init()
        {
            base.Init();
            //mFilters = new MeshFilter[PlaneCount * 4];
            //mRenderers = new MeshRenderer[PlaneCount * 4];




            CreateTop();
            CreateWrapper();
            CreateBottom();
            Normalize(mFilters, Radius);

            Divide();

            RecalculatePosition(mFilters);
            ChangeColor(mRenderers, color);
            RecalculateNormals(mFilters);

        }

        void CreateWrapper()
        {

            CurrentAngleX = 0;
            for (int i = 0; i < PlaneCount; i++)
            {
                GameObject mesh = new GameObject("mesh");
                mesh.transform.parent = transform;

                CalculatePointsSphere();

                mRenderers[MeshIndex] = mesh.AddComponent<MeshRenderer>();
                mFilters[MeshIndex] = mesh.AddComponent<MeshFilter>();
                PlaneCreator creator = new PlaneCreator(mFilters[MeshIndex].mesh, Resolution, Point0, Point1);
                creator.Create();

                MeshIndex++;
            }
        }

        void CreateTop()
        {
            CurrentAngleX = 0;
            for (int i = 0; i < PlaneCount; i++)
            {
                GameObject mesh = new GameObject("mesh");
                mesh.transform.parent = transform;

                CalculatePointsSphere();

                mRenderers[MeshIndex] = mesh.AddComponent<MeshRenderer>();
                mFilters[MeshIndex] = mesh.AddComponent<MeshFilter>();

                Center.y += .5f;
                FoundationTriangels TopCreator = new FoundationTriangels(mFilters[MeshIndex].mesh, Resolution, Point0, Point1, Center, Radius, true);
                TopCreator.Create();
                Center.y = 0;

                MeshIndex++;
            }

        }

        void CreateBottom()
        {
            CurrentAngleX = 0;
            for (int i = 0; i < PlaneCount; i++)
            {
                GameObject mesh = new GameObject("mesh");
                mesh.transform.parent = transform;

                CalculatePointsSphere();

                mRenderers[MeshIndex] = mesh.AddComponent<MeshRenderer>();
                mFilters[MeshIndex] = mesh.AddComponent<MeshFilter>();

                Center.y -= .5f;
                FoundationTriangels BottomCreator = new FoundationTriangels(mFilters[MeshIndex].mesh, Resolution, Point0, Point1, Center, Radius, false);
                BottomCreator.Create();
                Center.y = 0;

                MeshIndex++;
            }
        }

        void Divide()
        {

            float SphereHeight = (Height - (Radius*2)) / 2;
            for (int x = 0; x < PlaneCount; x++)
            {
                Vector3[] NewVertices = mFilters[x].mesh.vertices;
                for (int i = 0; i < mFilters[x].mesh.vertices.Length; i++)
                {
                    NewVertices[i].y +=  SphereHeight;
                }
                mFilters[x].mesh.vertices = NewVertices;
            }

            for (int x = PlaneCount * 2; x < PlaneCount*3; x++)
            {
                Vector3[] NewVertices = mFilters[x].mesh.vertices;
                for (int i = 0; i < mFilters[x].mesh.vertices.Length; i++)
                {
                    NewVertices[i].y -=  SphereHeight;
                }
                mFilters[x].mesh.vertices = NewVertices;
            }

            for (int x = PlaneCount; x < PlaneCount *2; x++)
            {
                Vector3[] NewVertices = mFilters[x].mesh.vertices;
                for (int i = 0; i < mFilters[x].mesh.vertices.Length; i++)
                {
                    if (i < (Resolution + 1)*(Resolution + 1)/2)
                    {
                        NewVertices[i].y -=  SphereHeight;
                    }
                    else
                    {
                        NewVertices[i].y +=  SphereHeight;
                    }
                }
                mFilters[x].mesh.vertices = NewVertices;
            }

        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.green;

        //    for (int x = 0; x < mFilters.Length; x++)
        //    {
        //        foreach (Vector3 i in mFilters[x].mesh.vertices)
        //        {
        //            Gizmos.DrawSphere(i, .25f);
        //        }
        //    }
        //}
    }
}
