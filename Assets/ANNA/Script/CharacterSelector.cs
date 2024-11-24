using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public string character; // Isi di Inspector sesuai nama karakter (misalnya "Farisz", "Rosa", dll.)

    public void SelectCharacter()
    {
        // Simpan pilihan karakter di GameManager
        GameManager.Instance.SetCharacter(character);

        // Pindah ke Gameplay Scene setelah memilih karakter
        SceneManager.LoadScene("SnowForest");
    }
}
