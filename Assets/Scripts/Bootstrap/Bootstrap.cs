using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Intellectika.BootstrapSpace
{
    public class Bootstrap : MonoBehaviour // Здесь должна была быть инициализация служб и загрузка мейн уровня
    {

        private IEnumerator Start()
        {
            yield return new WaitForSecondsRealtime(.2f);
            AsyncOperation load = SceneManager.LoadSceneAsync(1);
        }
    }
}
