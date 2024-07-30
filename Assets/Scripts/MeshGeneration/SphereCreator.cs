using Intellectika.Geometry;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    public class SphereCreator : MonoBehaviour
    {

      //public  int pointCount;



      // void UpdateMesh()
      //  {
            
      //  }


      //  void calculate()
      //  {
      //      int counter = (int)Mathf.Sqrt(pointCount);
      //      for (int y = 0; y < counter; y++)
      //      {
      //          for (int x = 0; x < counter; x++)
      //          {
                    
      //          }
      //      }
                
      //  }


      //  public void CreateSphereBottom()
      //  {


      //      for (int y = 0; y < StepY; y++)
      //      {
      //          for (int x = 0; x < StepX; x++)
      //          {
      //              planevectors = RecalculateSphereVectors();
      //              SetSphereSingle(x, y);
      //              CreatePlaneTriangels(x);

      //              CurrentAngleX += SingleAngle;



      //              SetFoundationVertices(y);
      //              CurrentAngleY += SingleAngle;

      //              VerticeIndex += StepX + 1;
      //          }
      //          VerticeIndex += StepX + 1;
      //      }

      //  }


      //  public void SetSphereSingle(int CurrentX, int CurrentY)
      //  {
      //      Vertices[CurrentX + VerticeIndex] = planevectors.v00;
      //      Vertices[CurrentX + VerticeIndex + StepX + 1] = planevectors.v01;
      //      Vertices[CurrentX + VerticeIndex + 1] = planevectors.v10;
      //      Vertices[CurrentX + VerticeIndex + StepX + 2] = planevectors.v11;
      //  }


      //  public PlaneVectors RecalculateSphereVectors()
      //  {
      //      PlaneVectors vectors = planevectors;
      //      vectors.v00 = geometry.CalculateSpherePoint(center, Radius, CurrentAngleX, CurrentAngleY);
      //      vectors.v01 = geometry.CalculateSpherePoint(center, Radius, CurrentAngleX, CurrentAngleY);
      //      vectors.v10 = geometry.CalculateSpherePoint(center, Radius, CurrentAngleX, CurrentAngleY);
      //      vectors.v11 = geometry.CalculateSpherePoint(center, Radius, CurrentAngleX, CurrentAngleY);


      //      return vectors;
      //  }
    }
}
