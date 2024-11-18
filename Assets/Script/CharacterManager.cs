using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject characterPrefab; // Prefab karakter
    public LayerMask tileLayerMask; // Layer untuk tile agar karakter hanya mendeteksi collider tile

    private GameObject characterInstance;
    private BoxCollider2D characterCollider;

    void Start()
    {
        // Cek apakah karakter sudah diinstansiasi
        if (characterInstance == null)
        {
            // Buat instans karakter dari prefab
            characterInstance = Instantiate(characterPrefab, transform.position, Quaternion.identity);
            characterCollider = characterInstance.GetComponent<BoxCollider2D>();

            // Atur ukuran collider awal
            AdjustColliderSize();
        }
    }

    void AdjustColliderSize()
    {
        // Cast ray ke bawah untuk mendeteksi tile yang ada di bawah karakter
        RaycastHit2D hit = Physics2D.Raycast(characterInstance.transform.position, Vector2.down, Mathf.Infinity, tileLayerMask);

        if (hit.collider != null)
        {
            // Jika collider ditemukan, ambil ukuran tile dari collider
            BoxCollider2D tileCollider = hit.collider.GetComponent<BoxCollider2D>();

            if (tileCollider != null)
            {
                // Atur ukuran collider karakter agar sesuai dengan ukuran tile yang ditemui
                characterCollider.size = new Vector2(tileCollider.size.x, tileCollider.size.y);

                // Opsional: Menyesuaikan posisi collider agar tepat di tengah tile
                characterCollider.offset = new Vector2(0, tileCollider.offset.y);
            }
            else
            {
                Debug.LogWarning("Tile collider tidak memiliki BoxCollider2D");
            }
        }
        else
        {
            Debug.LogWarning("Tidak ada tile yang terdeteksi di bawah karakter");
        }
    }
}
