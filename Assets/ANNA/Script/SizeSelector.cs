using UnityEngine;

public class SizeSelector : MonoBehaviour
{
    public string size; // Isi di Inspector sesuai ukuran (misalnya "7x3", "4x4")
    private UIManager uiManager; // Referensi ke UIManager
    public GameObject panelSelectSize; // Referensi ke panel pemilihan ukuran

    void Awake()
    {
        // Mencari UIManager di scene dan menyimpannya ke variabel
        uiManager = FindObjectOfType<UIManager>();
    }

    public void SelectSize()
    {
        // Simpan pilihan ukuran di GameManager
        GameManager.Instance.SetSize(size);

        // Pastikan uiManager tidak null sebelum memanggil ShowCharaPanel
        if (uiManager != null)
        {
            uiManager.ShowCharaPanel();
        }
        else
        {
            Debug.LogError("UIManager tidak ditemukan di scene.");
        }

        // Menonaktifkan panel pemilihan ukuran
        if (panelSelectSize != null)
        {
            panelSelectSize.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel pemilihan ukuran tidak diatur di SizeSelector.");
        }
    }
}
