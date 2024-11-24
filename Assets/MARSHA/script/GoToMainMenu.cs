using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;  

    public void Home()
    {
       
        SceneManager.LoadScene("Mainmenu");
        Time.timeScale = 1;  
    }

}
