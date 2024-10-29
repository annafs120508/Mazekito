using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Mengganti nama scene ke "loadscene"
        SceneManager.LoadScene("loadscene");
    }
}
