using UnityEngine;
using UnityEngine.UI;

public class MusicToggleWithImage : MonoBehaviour
{
    public AudioSource musicSource;     // Referensi ke AudioSource
    public Button toggleButton;         // Referensi ke Button UI
    public Sprite musicOnImage;         // Gambar untuk kondisi On
    public Sprite musicOffImage;        // Gambar untuk kondisi Off

    private bool isPlaying = true;      // Status musik (On atau Off)
    private Image buttonImage;          // Komponen gambar dari button

    void Start()
    {
        // Ambil komponen Image dari button
        buttonImage = toggleButton.GetComponent<Image>();
        // Set gambar awal sesuai status
        buttonImage.sprite = musicOnImage;

        // Tambahkan listener untuk aksi tombol
        toggleButton.onClick.AddListener(ToggleMusic);
    }

    // Fungsi untuk On/Off musik dan mengubah gambar
    void ToggleMusic()
    {
        if (isPlaying)
        {
            musicSource.Pause();                   // Jika musik sedang main, pause
            buttonImage.sprite = musicOffImage;    // Ubah gambar ke Off
        }
        else
        {
            musicSource.Play();                    // Jika musik mati, nyalakan lagi
            buttonImage.sprite = musicOnImage;     // Ubah gambar ke On
        }
        isPlaying = !isPlaying;                    // Ubah status isPlaying
    }
}
