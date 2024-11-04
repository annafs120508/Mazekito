using UnityEngine;

public class SizeSelector : MonoBehaviour
{
    public string size; // Isi di Inspector sesuai ukuran (misalnya "7x3", "4x4")
    public GameObject panelSelectChara; // Referensi ke panel karakter

    public void SelectSize()
    {
        // Simpan pilihan ukuran di GameManager
        GameManager.Instance.SetSize(size);

        // Menonaktifkan panel ukuran
        gameObject.SetActive(false);

        // Mengaktifkan panel karakter
        panelSelectChara.SetActive(true); // Mengaktifkan panel_selectchara
    }
}
