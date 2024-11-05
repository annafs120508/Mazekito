using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoader: MonoBehaviour
{
    // GameObject untuk loading screen
    public GameObject loadingScreen;

    // Slider untuk menunjukkan progress
    public Slider loadingBar;

    // Fungsi untuk memulai proses load scene
    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    // Coroutine untuk proses loading secara asynchronous
    IEnumerator LoadSceneAsync(int sceneId)
    {
        // Aktifkan loading screen
        loadingScreen.SetActive(true);

        // Mulai proses load scene secara asynchronous
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        // Selama scene belum selesai dimuat
        while (!operation.isDone)
        {
            // Hitung progress dari 0 hingga 1
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            // Update nilai slider sesuai progress loading
            loadingBar.value = progressValue;

            // Tunggu hingga frame berikutnya
            yield return null;
        }
    }
}
