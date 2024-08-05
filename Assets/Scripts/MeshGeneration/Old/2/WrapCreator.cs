using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using geometry = Intellectika.Geometry.geometry;

namespace Intellectika
{
    //[RequireComponent(typeof(PlaneCreator) , typeof(FoundationCreator))]
    //public class WrapCreator : MeshData
    //{

    //    _PlaneCreator.PlaneVectors _PlaneVectors = new _PlaneCreator.PlaneVectors();
    //    private void Awake()
    //    {
    //        SingleAngle = 360 / PlaneCount;
    //        Triangels = new int[ (StepX +1 ) + (StepY+1) * 6 * PlaneCount];

    //        Vertices = new Vector3[(StepX + 1) + (StepY + 1) * PlaneCount];


    //         both = new int[ (StepX + StepY + 2) * PlaneCount] ;
    //        top = new int[(StepX + StepY + 2) * PlaneCount];
    //        if (planecreator != null ) 
    //        {
    //            _PlaneVectors.v01 = new Vector3(0, Height, 0);
    //            UpdateMesh(StepX, StepY);
    //            Debug.Log(TriangleIndex);
    //            Debug.Log(VerticeIndex);
    //        }
    //    }


    //    void UpdateMesh(int X, int Y)
    //    {


    //        for (int i = 0; i < PlaneCount; i++)
    //        {
    //            //_PlaneVectors = RecalculatePlaneVectors(_PlaneVectors, Radius, ref CurrentAngleX);
    //           // _PlaneVectors = RecalculateSphereVectors(_PlaneVectors, Radius, ref CurrentAngleY, ref CurrentAngleX);
    //         //   planecreator.CreateSphere(ref Triangels,ref Vertices, ref TriangleIndex, ref VerticeIndex, _PlaneVectors, X, Y, ref both, ref top, ref iteration, Radius,ref CurrentAngleY,ref CurrentAngleX, SingleAngle);
    //            Debug.Log(i);
                
    //        }
    //      //  TriangleIndex = foundationCreator.CreateBoth(ref Triangels, TriangleIndex, ref VerticeIndex, both, Vertices);
    //      //  TriangleIndex = foundationCreator.CreateTop(ref Triangels, TriangleIndex, ref VerticeIndex, top, Vertices, Height);
    //        mesh.Clear();
    //        mesh.vertices = Vertices;
    //        mesh.triangles = Triangels;
    //        //Debug.Log(TriangleIndex);
    //       // Debug.Log(VerticeIndex);
    //    }

    //    PlaneVectors RecalculatePlaneVectors(PlaneVectors vectors, int radius, ref float currentangaleX)
    //    {
    //        vectors.v00 = new Vector3( radius * geometry.cos(currentangaleX - SingleAngle/2), vectors.v00.y, radius * geometry.sin(currentangaleX - SingleAngle/2));
    //        vectors.v01 = new Vector3(vectors.v00.x, vectors.v01.y, vectors.v00.z);
    //        vectors.v10 = new Vector3( radius * geometry.cos(currentangaleX + SingleAngle/2), vectors.v00.y,  radius * geometry.sin(currentangaleX + SingleAngle/2));
    //       vectors.v11 = new Vector3(vectors.v10.x, vectors.v01.y, vectors.v10.z);
    //       // Debug.Log(vectors.v00 + "  " + vectors.v10);

    //        currentangaleX += SingleAngle;
    //        return vectors;
    //        //z = R * sin(угол)
    //        //x = R * cos(угол)
    //    }



    //    private void OnDrawGizmos()
    //    {
    //        Gizmos.color = Color.green;
    //        foreach (Vector3 i in Vertices)
    //        {
    //            Gizmos.DrawSphere(i + center, 0.25f);
    //        }
    //    }
    //}
}
