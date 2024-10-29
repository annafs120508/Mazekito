using UnityEngine;

public class TiltControl : MonoBehaviour
{
    public float speed = 50.0f;         // Kecepatan bola, ditingkatkan untuk memastikan bola bergerak
    public float tiltThreshold = 0.01f; // Threshold untuk mendeteksi tilt yang cukup besar
    public Vector3 boundaryMin;         // Batas minimum area pembatas (x, y, z)
    public Vector3 boundaryMax;         // Batas maksimum area pembatas (x, y, z)

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    void FixedUpdate()
    {
        // Baca input dari akselerometer
        Vector3 tilt = Input.acceleration;

        // Hanya gunakan tilt pada bidang x dan z
        Vector3 tiltDirection = new Vector3(tilt.x, 0, tilt.y);

        // Log tilt direction untuk debugging
        Debug.Log("Tilt Direction: " + tiltDirection);

        // Jika tilt lebih besar dari threshold, gerakkan bola
        if (tiltDirection.magnitude > tiltThreshold)
        {
            rb.AddForce(tiltDirection * speed, ForceMode.Acceleration);
        }

        // Batasi posisi bola hanya jika keluar dari area pembatas
        Vector3 currentPosition = rb.position;

        // Periksa apakah posisi melebihi batasan
        if (currentPosition.x < boundaryMin.x || currentPosition.x > boundaryMax.x ||
            currentPosition.z < boundaryMin.z || currentPosition.z > boundaryMax.z)
        {
            currentPosition.x = Mathf.Clamp(currentPosition.x, boundaryMin.x, boundaryMax.x);
            currentPosition.z = Mathf.Clamp(currentPosition.z, boundaryMin.z, boundaryMax.z);

            // Hentikan bola jika berada di batasan
            rb.position = currentPosition;
            rb.velocity = Vector3.zero;
        }
    }
}
