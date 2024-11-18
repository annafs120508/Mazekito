using UnityEngine;

public class Winpanelmanager : MonoBehaviour
{
    [SerializeField] private GameObject winPanel; // Panel yang akan ditampilkan saat menang

    private void Start()
    {
        // Nonaktifkan panel saat game dimulai
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    // Fungsi untuk menampilkan panel menang
    public void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f; // Hentikan waktu (opsional)
        }
    }
}
