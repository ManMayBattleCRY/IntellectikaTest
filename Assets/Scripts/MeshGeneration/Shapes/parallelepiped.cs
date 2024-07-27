using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    public class parallelepiped : MeshCreator
    {


        public override void CreateTriangle(int X, int Y, int Z)
        {
            int FacesCount = (X * Y + Y * Z + X * Z) * 2;
            Triangles = new int[FacesCount * 6];
            int loop = (X + Z) *2;

            int vIndex = 0;
            int tIndex = 0;
            for (int y = 0; y < Y; y++)
            {
                for (int l = 0; l < loop -1; l++) 
                {
                    tIndex = SetPlane(Triangles, tIndex, vIndex , vIndex + loop, vIndex + 1, vIndex + loop + 1);
                    vIndex++;
                }
                tIndex = SetPlane(Triangles, tIndex, vIndex, vIndex + loop, vIndex - loop +1, vIndex + 1);
                vIndex ++;
            }
        }

        public override Vector3[] CreateVertices(int X, int Y, int Z)
        {
            int Corners = 8;
            int Edges = (X + Y + Z - 3) * 4;
            int Faces = ((X - 1) * (Y - 1))
                        + ((X - 1) * (Z - 1))
                        + ((Y - 1) * (Z - 1));
            Faces *= 2;
            Vertices = new Vector3[Corners + Edges + Faces];


            int vIndex = 0;
            for (int y = 0; y <= Y; y++)
            {
                for (int x = 0; x <= X; x++)
                {
                    Vertices[vIndex] = new Vector3(x, y, 0);
                    vIndex++;
                }


                for (int z = 1; z <= Z; z++)
                {
                    Vertices[vIndex] = new Vector3(X, y, z);
                    vIndex++;
                }

                for (int x = X - 1; x >= 0; x--)
                {
                    Vertices[vIndex] = new Vector3(x, y, Z);
                    vIndex++;
                }


                for (int z = Z - 1; z > 0; z--)
                {
                    Vertices[vIndex] = new Vector3(0, y, z);
                    vIndex++;
                }
            }

            for (int x = 1; x < X; x++)
            {
                for (int z = 1; z < Z; z++)
                {
                    Vertices[vIndex] = new Vector3(x, Y, z);
                    vIndex++;
                    Vertices[vIndex] = new Vector3(x, 0, z);
                    vIndex++;
                }
            }

            return Vertices;

        }


        public int SetPlane(int[] triangles, int tIndex,int v00,int v01, int v10, int v11)
        {
                    Triangles[tIndex] = v00;
                    Triangles[tIndex + 1] = Triangles[tIndex + 4] = v01;
                    Triangles[tIndex + 2] = Triangles[tIndex + 3] = v10;
                    Triangles[tIndex + 5] = v11;
            return tIndex + 6;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (Vector3 i in CreateVertices(4, 4, 4))
            {
                Gizmos.DrawSphere(i, 0.25f);
            }
        }

        private void Awake()
        {
            int x = 4;
            int y = 4;
            int z = 4;

            Vertices = CreateVertices(x, y, z);
            CreateTriangle(x, y, z);

            Mesh mesh = GetComponent<MeshFilter>().mesh;
            mesh.vertices = Vertices;
            mesh.triangles = Triangles;
            mesh.RecalculateNormals();
        }

    }
}
