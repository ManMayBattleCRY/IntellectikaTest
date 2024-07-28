using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    namespace Geometry
    {
        public class geometry : MonoBehaviour
        {

            public static float sin(float angle)
            {
                return (float)Math.Sin(angle * Math.PI / 180);
            }

            public static float cos(float angle)
            {
                return (float)Math.Cos(angle * Math.PI / 180);
            }
        }
    }
}
