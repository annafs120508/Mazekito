using UnityEngine;
using UnityEngine.UI;

public class UnlockButtonController : MonoBehaviour
{
    public Button[] buttonsToUnlock; 

   
    public void UnlockButtons()
    {
        foreach (Button button in buttonsToUnlock)
        {
            button.interactable = true; // Membuka kunci tombol
        }
    }
}
