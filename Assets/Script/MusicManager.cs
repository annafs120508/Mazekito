using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;  // Singleton untuk memastikan hanya ada satu MusicManager

    void Awake()
    {
        // Jika tidak ada instance lain dari MusicManager, set sebagai instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Jangan hancurkan musik saat berpindah scene
        }
        else
        {
            Destroy(gameObject);  // Jika sudah ada instance, hancurkan object yang baru
        }
    }
}
