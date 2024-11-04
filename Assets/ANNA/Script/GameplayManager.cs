using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameObject[] panels; // Daftar semua panel yang tersedia di scene ini

    private void Start()
    {
        // Ambil ukuran dan karakter yang dipilih dari GameManager
        string selectedSize = GameManager.Instance.GetSelectedSize();
        string selectedCharacter = GameManager.Instance.GetSelectedCharacter();

        // Buat nama panel sesuai dengan pilihan
        string panelName = "Panel_Size" + selectedSize + "_Character" + selectedCharacter;

        // Cari dan aktifkan panel yang sesuai
        foreach (GameObject panel in panels)
        {
            if (panel.name == panelName)
            {
                panel.SetActive(true); // Aktifkan panel yang sesuai
            }
            else
            {
                panel.SetActive(false); // Nonaktifkan panel lain
            }
        }
    }
}
