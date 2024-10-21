using UnityEngine;
using System.Collections.Generic;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject currentActivePanel; // Panel yang saat ini aktif
    private Stack<GameObject> panelHistory = new Stack<GameObject>(); // Menyimpan riwayat panel

    // Metode untuk beralih ke panel yang baru
    public void SwitchToPanel(GameObject newPanel)
    {
        // Jika ada panel yang aktif saat ini, simpan ke riwayat
        if (currentActivePanel != null)
        {
            panelHistory.Push(currentActivePanel);
            currentActivePanel.SetActive(false);
        }

        // Aktifkan panel baru
        newPanel.SetActive(true);
        currentActivePanel = newPanel;
    }

    // Metode untuk kembali ke panel sebelumnya
    public void SwitchBackToPreviousPanel()
    {
        // Pastikan ada panel dalam riwayat
        if (panelHistory.Count > 0)
        {
            // Sembunyikan panel yang aktif saat ini
            if (currentActivePanel != null)
            {
                currentActivePanel.SetActive(false);
            }

            // Ambil panel sebelumnya dari riwayat
            currentActivePanel = panelHistory.Pop();
            currentActivePanel.SetActive(true);
        }
    }
}
