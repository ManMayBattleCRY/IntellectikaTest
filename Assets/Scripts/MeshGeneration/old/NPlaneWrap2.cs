using Intellectika.Geometry;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Intellectika
{
    //internal class NPlaneWrap2 : MeshObjects
    //{
    //    [SerializeField] Color color;
    //    [SerializeField] float Radius = 4f;
    //    float CurrentAngleX = 0; 
    //    float SingleAngle = 0;
    //    [SerializeField, Range(3,50)] int PlaneCount = 3;
    //    MeshFilter[] mFilters;
    //    MeshRenderer[] mRenderers;

    //    internal Vector3 Point0;
    //    internal Vector3 Point1;

    //    Vector3 Center;

    //    void CalculatePoints()
    //    {
    //        Point0 = new Vector3(.5f * geometry.cos(CurrentAngleX - SingleAngle / 2), - .5f, .5f * geometry.sin(CurrentAngleX - SingleAngle / 2));
    //        Point1 = new Vector3(.5f * geometry.cos(CurrentAngleX + SingleAngle / 2), .5f, .5f * geometry.sin(CurrentAngleX + SingleAngle / 2));
    //        CurrentAngleX += SingleAngle;
    //    }

    //    internal override void Awake()
    //    {
    //        base.Awake();
    //        SingleAngle = 360/(float)PlaneCount;
    //        mFilters = new MeshFilter[PlaneCount* 3];
    //        mRenderers = new MeshRenderer[PlaneCount * 3];
    //        Wrap();
    //        Recalculate();
    //    }

    //    void Wrap()
    //    {
    //        int MeshIndex = 0;
    //        Center = transform.position;
    //        for (int i = 0; i < PlaneCount; i++)
    //        {
    //            GameObject mesh = new GameObject("mesh");
    //            mesh.transform.parent = transform;

    //            CalculatePoints();

    //            mRenderers[i] = mesh.AddComponent<MeshRenderer>();
    //            mFilters[i] = mesh.AddComponent<MeshFilter>();
    //            PlaneCreator creator = new PlaneCreator(mFilters[i].mesh, Resolution, Point0 , Point1);
    //            creator.Create();

    //            MeshIndex++;
    //        }
    //        CurrentAngleX = 0;
    //        for (int i = 0; i < PlaneCount; i++)
    //        {
    //            GameObject mesh = new GameObject("mesh");
    //            mesh.transform.parent = transform;

    //            CalculatePoints();

    //            mRenderers[MeshIndex] = mesh.AddComponent<MeshRenderer>();
    //            mFilters[MeshIndex] = mesh.AddComponent<MeshFilter>();

    //            Center.y += .5f;
    //            FoundationTriangels TopCreator = new FoundationTriangels(mFilters[MeshIndex].mesh, Resolution, Point0, Point1, Center, Radius, true);
    //            TopCreator.Create();
    //            Center = transform.position;

    //            MeshIndex++;
    //        }
    //        CurrentAngleX = 0;
    //        for (int i = 0; i < PlaneCount; i++)
    //        {
    //            GameObject mesh = new GameObject("mesh");
    //            mesh.transform.parent = transform;

    //            CalculatePoints();

    //            mRenderers[MeshIndex] = mesh.AddComponent<MeshRenderer>();
    //            mFilters[MeshIndex] = mesh.AddComponent<MeshFilter>();

    //            Center.y -= .5f;
    //            FoundationTriangels BottomCreator = new FoundationTriangels(mFilters[MeshIndex].mesh, Resolution, Point0, Point1, Center, Radius, false);
    //            BottomCreator.Create();
    //            Center = transform.position;

    //            MeshIndex++;
    //        }

    //    }



    //    void Recalculate()
    //    {
    //        for (int x = 0; x < mRenderers.Length; x++)
    //        {
    //            mRenderers[x].material = new Material(Shader.Find("Standard"));
    //            mRenderers[x].material.color = color;
    //        }

    //        for (int x = 0; x < mFilters.Length; x++)
    //        {
    //            Vector3[] NewVertices = mFilters[x].mesh.vertices;
    //            for (int i = 0; i < mFilters[x].mesh.vertices.Length; i++)
    //            {
    //                NewVertices[i] = NewVertices[i].normalized * Radius;
    //            }
    //            mFilters[x].mesh.vertices = NewVertices;
    //        }



    //        for (int x = 0; x < mFilters.Length; x++)
    //        {
    //            mFilters[x].mesh.RecalculateBounds();
    //            mFilters[x].mesh.RecalculateNormals();
    //        }
    //    }

    //}
}
