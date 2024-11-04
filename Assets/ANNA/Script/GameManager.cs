using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private string selectedSize;
    private string selectedCharacter;

    private void Awake()
    {
        // Membuat GameManager menjadi Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSize(string size)
    {
        selectedSize = size;
        Debug.Log("Size selected: " + size);
    }

    public void SetCharacter(string character)
    {
        selectedCharacter = character;
        Debug.Log("Character selected: " + character);
    }

    public string GetSelectedSize()
    {
        return selectedSize;
    }

    public string GetSelectedCharacter()
    {
        return selectedCharacter;
    }
}
