using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Intellectika.BootstrapSpace
{
    public class Bootstrap : MonoBehaviour // ����� ������ ���� ���� ������������� ����� � �������� ���� ������
    {

        private IEnumerator Start()
        {
            yield return new WaitForSecondsRealtime(.2f);
            AsyncOperation load = SceneManager.LoadSceneAsync(1);
        }
    }
}
