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


            public static Vector3 CalculateSpherePoint(Vector3 center, int radius, float Xangle, float Yangle)
            {
                float z = center.z + radius * cos(Yangle);
                float y = center.y + radius * sin(Yangle) * sin(Xangle);
                float x = center.x + radius * sin(Yangle) * cos(Xangle);
                // Yangle 0--180 ???

                return new Vector3(x, y, z);
            }
        }
    }
}
