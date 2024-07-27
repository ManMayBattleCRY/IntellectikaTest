using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class MeshGenerationTest : MonoBehaviour
    {
        Mesh mesh;
        Vector3[] vertices;
        int[] triangles;

        // Start is called before the first frame update
        void Awake()
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

            CreateShape();
            UpdateMesh();
        }


        void CreateShape()
        {
            vertices = new Vector3[]
                {
                    new Vector3(0,0,0),
                    new Vector3(0,0,1),
                    new Vector3(1,0,0)
                };

            triangles = new int[]
                {
                    0,1,2
                };
        }

        void UpdateMesh()
        {
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
        }
    }
}
