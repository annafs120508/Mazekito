using UnityEngine;

public class TiltControl : MonoBehaviour
{
    public float speed = 50.0f;         // Kecepatan bola, ditingkatkan untuk memastikan bola bergerak
    public float tiltThreshold = 0.01f; // Threshold untuk mendeteksi tilt yang cukup besar
    public Vector2 boundaryMin;         // Batas minimum area pembatas (x, y)
    public Vector2 boundaryMax;         // Batas maksimum area pembatas (x, y)

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
    }

    void FixedUpdate()
    {
        // Baca input dari akselerometer
        Vector2 tilt = new Vector2(Input.acceleration.x, Input.acceleration.y);

        // Hanya gunakan tilt pada bidang x dan z
        Vector2 tiltDirection = new Vector2(tilt.x, tilt.y);

        // Log tilt direction untuk debugging
        Debug.Log("Tilt Direction: " + tiltDirection);

        // Jika tilt lebih besar dari threshold, gerakkan bola
        if (tiltDirection.magnitude > tiltThreshold)
        {
            rb.AddForce(tiltDirection * speed, ForceMode2D.Force);
        }

        // Batasi posisi bola hanya jika keluar dari area pembatas
        Vector2 currentPosition = rb.position;

        // Periksa apakah posisi melebihi batasan
        if (currentPosition.x < boundaryMin.x || currentPosition.x > boundaryMax.x ||
            currentPosition.y < boundaryMin.y || currentPosition.y > boundaryMax.y)
        {
            currentPosition.x = Mathf.Clamp(currentPosition.x, boundaryMin.x, boundaryMax.x);
            currentPosition.y = Mathf.Clamp(currentPosition.y, boundaryMin.y, boundaryMax.y);

            // Hentikan bola jika berada di batasan
            rb.position = currentPosition;
            rb.velocity = Vector2.zero;
        }
    }
}
