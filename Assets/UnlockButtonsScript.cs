using UnityEngine;
using UnityEngine.UI;

public class UnlockButtonsScript : MonoBehaviour
{
    public Button[] lockedButtons;      // Array untuk tombol-tombol yang akan di-unlock
    public Button unlockAllButton;      // Tombol Unlock All
    private bool isUnlocked = false;    // Status apakah tombol-tombol dalam keadaan unlocked

    void Start()
    {
        LockAllButtons(); // Mulai dengan mengunci semua tombol

        // Tambahkan listener pada tombol Unlock All
        unlockAllButton.onClick.AddListener(ToggleUnlock);
    }

    // Fungsi untuk mengunci semua tombol
    void LockAllButtons()
    {
        foreach (Button button in lockedButtons)
        {
            button.interactable = false; // Mengunci tombol
        }
        unlockAllButton.interactable = true; // Tombol Unlock All aktif lagi
        isUnlocked = false;
    }

    // Fungsi untuk membuka semua tombol
    void UnlockAllButtons()
    {
        foreach (Button button in lockedButtons)
        {
            button.interactable = true; // Membuka kunci tombol
        }
        unlockAllButton.interactable = false; // Disable tombol Unlock All agar tidak bisa digunakan
        isUnlocked = true;
    }

    // Fungsi untuk mengaktifkan/mengunci ulang berdasarkan status saat ini
    void ToggleUnlock()
    {
        if (isUnlocked)
        {
            LockAllButtons(); // Jika sudah terbuka, maka kunci ulang semua tombol
        }
        else
        {
            UnlockAllButtons(); // Jika terkunci, buka kunci semua tombol
        }
    }
}
