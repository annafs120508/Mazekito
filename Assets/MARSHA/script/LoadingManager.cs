using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar; // Slider UI untuk progress
    public Text progressText;  // UI Text untuk menampilkan persentase

    void Start()
    {
        // Mulai loading scene tujuan
        StartCoroutine(LoadAsyncScene("GameScene"));  // Ganti dengan nama scene gameplay kamu
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        // Mulai load scene secara asynchronous
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Supaya loading tidak otomatis selesai sebelum progress 100%
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Update progress bar dan text dengan nilai 0 sampai 1 (dikonversi jadi persen)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            progressText.text = (progress * 100f).ToString("F0") + "%";  

            // Kalau progress sudah 100%, baru aktifkan scene
            if (progress >= 1f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
