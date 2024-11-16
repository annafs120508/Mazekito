using UnityEngine;
using UnityEngine.Events;

public class SizeSelector : MonoBehaviour
{
    private int _mazeWidth;
    private int _mazeHeight;
    private UIManager uiManager; // Referensi ke UIManager
    public GameObject panelSelectSize; // Referensi ke panel pemilihan ukuran

    private void Start()
    {
        _mazeWidth = PlayerPrefs.GetInt("MazeWidth", 20);
        _mazeHeight = PlayerPrefs.GetInt("MazeHeight", 20);

    }

    public void SetSize(int width, int height)
    {
        _mazeWidth = width;
        _mazeHeight = height;
        saveSize();
    }

    public void saveSize()
    {
        PlayerPrefs.SetInt("MazeWidth", _mazeWidth);
        PlayerPrefs.SetInt("MazeHeight", _mazeHeight);
        PlayerPrefs.Save();
        Debug.Log($"Saved maze size: {_mazeWidth}x{_mazeHeight}");
    }

    public void Small()
    {
        SetSize(7, 3);
        saveSize();
    }

    public void Medium()
    {
        SetSize(9, 4);
        saveSize();
    }

    public void SemiLarge()
    {
        SetSize(13, 6);
        saveSize();
    }

    public void Large()
    {
        SetSize(20, 9);
        saveSize();
    }

    public void Large2()
    {
        SetSize(27, 12);
        saveSize();
    }

    public void Giant()
    {
        SetSize(36, 16);
        saveSize();
    }

    void Awake()
    {
        // Mencari UIManager di scene dan menyimpannya ke variabel
        uiManager = FindObjectOfType<UIManager>();
    }

    public void SelectSize()
    {

        // Pastikan uiManager tidak null sebelum memanggil ShowCharaPanel
        if (uiManager != null)
        {
            uiManager.ShowCharaPanel();
        }
        else
        {
            Debug.LogError("UIManager tidak ditemukan di scene.");
        }

        // Menonaktifkan panel pemilihan ukuran
        if (panelSelectSize != null)
        {
            panelSelectSize.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel pemilihan ukuran tidak diatur di SizeSelector.");
        }
    }
}
