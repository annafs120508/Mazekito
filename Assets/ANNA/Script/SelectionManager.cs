using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    public GameObject sizeSelectionPanel;
    public GameObject characterSelectionPanel;

    void Start()
    {
        // Tampilkan panel ukuran di awal
        sizeSelectionPanel.SetActive(true);
        characterSelectionPanel.SetActive(false);
    }

    // Fungsi untuk memilih ukuran
    public void OnSizeSelected(string size)
    {
        PlayerPrefs.SetString("SelectedSize", size); // Simpan ukuran ke PlayerPrefs
        sizeSelectionPanel.SetActive(false); // Sembunyikan panel ukuran
        characterSelectionPanel.SetActive(true); // Tampilkan panel karakter
    }

    // Fungsi untuk memilih karakter dan langsung pindah ke scene gameplay
    public void OnCharacterSelected(string character)
    {
        PlayerPrefs.SetString("SelectedCharacter", character); // Simpan karakter ke PlayerPrefs

        // Pindah ke scene gameplay
        SceneManager.LoadScene("GameplayScene");
    }
}
