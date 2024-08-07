using UnityEngine;

namespace Intellectika
{
    public class PressLoadingScreen : MonoBehaviour // убрать загрузачный экран с мейн сцены
    {
        public GameObject FirstCamera;
        public GameObject SecondCamera;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                FirstCamera.SetActive(false);
                SecondCamera.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
