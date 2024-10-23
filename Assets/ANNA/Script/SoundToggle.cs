using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public AudioSource soundEffectSource;  // AudioSource untuk sound effect
    public AudioClip clickSound;           // AudioClip untuk suara klik
    public Button soundToggleButton;       // Button untuk toggle On/Off sound
    public Sprite soundOnImage;            // Gambar untuk kondisi sound On
    public Sprite soundOffImage;           // Gambar untuk kondisi sound Off

    private bool isSoundOn = true;         // Status apakah sound effect aktif
    private Image buttonImage;             // Gambar tombol toggle

    void Start()
    {
        // Ambil komponen Image dari button
        buttonImage = soundToggleButton.GetComponent<Image>();
        // Set gambar awal sesuai status (On)
        buttonImage.sprite = soundOnImage;

        // Tambahkan listener untuk tombol toggle sound
        soundToggleButton.onClick.AddListener(ToggleSoundEffect);
    }

    // Fungsi untuk memainkan sound effect ketika tombol dipencet
    public void PlaySoundEffect()
    {
        if (isSoundOn && clickSound != null)  // Jika sound effect aktif dan ada AudioClip
        {
            soundEffectSource.PlayOneShot(clickSound);
        }
    }

    // Fungsi untuk mengubah status On/Off sound effect
    void ToggleSoundEffect()
    {
        isSoundOn = !isSoundOn;  // Ubah status On/Off

        if (isSoundOn)
        {
            buttonImage.sprite = soundOnImage;  // Ubah gambar ke kondisi On
        }
        else
        {
            buttonImage.sprite = soundOffImage; // Ubah gambar ke kondisi Off
        }
    }
}