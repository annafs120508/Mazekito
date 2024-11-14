using UnityEngine;

public class Tiltcb12 : MonoBehaviour
{
    public float tiltSpeed = 5f;          // Kecepatan pergerakan bola berdasarkan tilt
    public float tiltThreshold = 0.1f;    // Ambang batas tilt untuk mulai menggerakkan bola
    public float maxSpeed = 10f;          // Batas kecepatan maksimum bola

    public float xLimit = 10f;            // Batas pergerakan pada sumbu X
    public float yLimit = 5f;             // Batas pergerakan pada sumbu Y

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        // Mengunci posisi Z agar bola tidak bergerak di sumbu Z
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        // Dapatkan nilai kemiringan dari sensor akselerometer perangkat
        float tiltX = Input.acceleration.x;
        float tiltY = Input.acceleration.y;

        if (Mathf.Abs(tiltX) > tiltThreshold || Mathf.Abs(tiltY) > tiltThreshold)
        {
            Vector3 tiltForce = new Vector3(tiltX, tiltY, 0) * tiltSpeed;
            rb.AddForce(tiltForce, ForceMode.Acceleration);
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Batasi pergerakan bola pada sumbu X saja jika ingin y bebas bergerak, atau dari -yLimit ke yLimit
        Vector3 position = rb.position;
        position.x = Mathf.Clamp(position.x, -xLimit, xLimit);
        position.y = Mathf.Clamp(position.y, -yLimit, yLimit);
        rb.position = position;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Tambahkan logika jika bola bertabrakan dengan dinding maze, jika diperlukan
    }
}
