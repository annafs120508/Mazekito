using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance; // Singleton instance

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject); // Hancurkan instance baru jika sudah ada yang aktif
        }
    }

    public void PlayMusic()
    {
        // Contoh fungsi untuk memainkan musik
        GetComponent<AudioSource>().Play();
    }

    public void StopMusic()
    {
        // Contoh fungsi untuk menghentikan musik
        GetComponent<AudioSource>().Stop();
    }
}
