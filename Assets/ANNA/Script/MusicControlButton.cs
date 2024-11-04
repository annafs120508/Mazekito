using UnityEngine;
using UnityEngine.UI;

public class MusicControlButton : MonoBehaviour
{
    public Button playButton;
    public Button stopButton;

    private void Start()
    {
        // Periksa apakah MusicManager.Instance sudah ada
        if (MusicManager.Instance == null)
        {
            Debug.LogError("MusicManager instance not found! Make sure MusicManager is loaded in the scene.");
            return;
        }

        // Hubungkan tombol ke fungsi MusicManager
        playButton.onClick.AddListener(() => MusicManager.Instance.PlayMusic());
        stopButton.onClick.AddListener(() => MusicManager.Instance.StopMusic());
    }
}
