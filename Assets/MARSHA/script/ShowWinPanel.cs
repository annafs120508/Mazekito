using Unity.VisualScripting;
using UnityEngine;

public class ShowWinPanel : MonoBehaviour
{
    public GameObject winPanel; // Referensi ke panel Win

    private void Start()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered by: " + collision.gameObject.name); 

        if (collision.gameObject.CompareTag("Player"))
        { 
            if (winPanel != null)
            {
                winPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}