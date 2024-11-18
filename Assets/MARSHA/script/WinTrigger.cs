using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public Winpanelmanager winPanelManager; // Pastikan nama variabel menggunakan nama kelas Winpanelmanager

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (winPanelManager != null)
            {
                winPanelManager.ShowWinPanel();
            }
            else
            {
                Debug.LogError("WinPanelManager belum di-assign!");
            }
        }
    }
}
