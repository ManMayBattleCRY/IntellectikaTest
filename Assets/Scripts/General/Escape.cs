using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Intellectika
{
    public class Escape : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }
    }
}
