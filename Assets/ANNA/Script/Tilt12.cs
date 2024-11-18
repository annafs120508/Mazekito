using UnityEngine;

public class Tiltcb12 : MonoBehaviour
{
    public float tiltSpeed = 5f;          // Kecepatan pergerakan bola berdasarkan tilt
    public float tiltThreshold = 0.1f;   // Ambang batas tilt untuk mulai menggerakkan bola
    public float maxSpeed = 10f;         // Batas kecepatan maksimum bola

    public float xLimit = 10f;           // Batas pergerakan pada sumbu X
    public float yLimit = 5f;            // Batas pergerakan pada sumbu Y

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        // Mengunci rotasi agar bola tidak berputar
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void FixedUpdate()
    {
        // Dapatkan nilai kemiringan dari sensor akselerometer perangkat
        float tiltX = Input.acceleration.x;
        float tiltY = Input.acceleration.y;

        // Hanya tambahkan gaya jika tilt melampaui ambang batas
        if (Mathf.Abs(tiltX) > tiltThreshold || Mathf.Abs(tiltY) > tiltThreshold)
        {
            Vector2 tiltForce = new Vector2(tiltX, tiltY) * tiltSpeed;
            rb.AddForce(tiltForce, ForceMode2D.Force);
        }

        // Batasi kecepatan maksimum bola
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Batasi pergerakan bola dalam area tertentu tanpa mengatur langsung rb.position
        Vector2 clampedPosition = rb.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -xLimit, xLimit);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -yLimit, yLimit);

        // Terapkan posisi yang ter-clamp hanya jika diperlukan
        if (rb.position != clampedPosition)
        {
            rb.velocity = Vector2.zero; // Hentikan pergerakan jika berada di luar batas
            rb.position = clampedPosition;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Tambahkan logika jika bola bertabrakan dengan dinding maze, jika diperlukan
        Debug.Log("Collided with: " + collision.gameObject.name);
    }
}
