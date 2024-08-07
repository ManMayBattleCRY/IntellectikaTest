using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Intellectika
{
    public class Bootstrap : MonoBehaviour
    {

        private IEnumerator Start()
        {
            yield return new WaitForSecondsRealtime(.2f);
            AsyncOperation load = SceneManager.LoadSceneAsync(1);
        }
    }
}
