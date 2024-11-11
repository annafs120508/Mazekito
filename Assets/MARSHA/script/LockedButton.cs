using UnityEngine;
using UnityEngine.UI;

public class LockedButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.interactable = false; // Memulai tombol dalam keadaan terkunci
    }
}
