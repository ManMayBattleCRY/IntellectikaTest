using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    internal class MeshObjects : MonoBehaviour
    {
        [HideInInspector]internal Mesh mesh;
        [Range(2, 256)]
        [SerializeField]internal int Resolution = 8;

        internal virtual void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;
        }
    }
}
