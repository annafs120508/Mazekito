using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject charaPanel; // Panel karakter yang akan ditampilkan

    // Fungsi untuk menampilkan panel karakter
    public void ShowCharaPanel()
    {
        if (charaPanel != null)
        {
            charaPanel.SetActive(true); // Mengaktifkan panel karakter
        }
        else
        {
            Debug.LogError("Chara Panel tidak diatur di UIManager.");
        }
    }

    // Fungsi untuk menyembunyikan panel karakter
    public void HideCharaPanel()
    {
        if (charaPanel != null)
        {
            charaPanel.SetActive(false); // Menonaktifkan panel karakter
        }
    }
}
