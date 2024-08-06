using Palmmedia.ReportGenerator.Core.Parser.Filtering;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Intellectika
{
    internal class Prism : MeshObjects
    {
        [SerializeField] private protected float Height = 4;

        private void Awake()
        {
            Init();
        }

        private protected override void Init()
        {
            base.Init();



            CreateWrapper();
            CreateTop();
            CreateBottom();
            RecalculateSize(mFilters, 1, Height, 1);
            ChangePrismSize(mFilters, Radius);
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

                CalculatePoints();

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

                CalculatePoints();

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

                CalculatePoints();

                mRenderers[MeshIndex] = mesh.AddComponent<MeshRenderer>();
                mFilters[MeshIndex] = mesh.AddComponent<MeshFilter>();

                Center.y -= .5f;
                FoundationTriangels BottomCreator = new FoundationTriangels(mFilters[MeshIndex].mesh, Resolution, Point0, Point1, Center, Radius, false);
                BottomCreator.Create();
                Center.y = 0;

                MeshIndex++;
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