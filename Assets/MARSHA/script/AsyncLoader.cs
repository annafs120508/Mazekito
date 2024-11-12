using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public float delayTime = 2f;

    public void LoadScene(int sceneId)
    {
        // Pastikan time scale diatur ke 1 sebelum load scene
        Time.timeScale = 1f;
        StartCoroutine(ShowLoadingScreenAndLoadScene(sceneId));
    }

    IEnumerator ShowLoadingScreenAndLoadScene(int sceneId)
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(delayTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progressValue;

            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1); // Tambahan waktu jika ingin delay lagi
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
