using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f;
    public float movementThreshold = 0.1f; // Nilai threshold untuk memulai pergerakan
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 acceleration = Input.acceleration;

        // Periksa jika akselerasi lebih besar dari threshold
        if (Mathf.Abs(acceleration.x) > movementThreshold || Mathf.Abs(acceleration.y) > movementThreshold)
        {
            Vector3 movement = new Vector3(acceleration.x, 0, acceleration.y);
            rb.AddForce(movement * speed);
        }
        else
        {
            rb.velocity = Vector3.zero; // Buat bola diam jika tidak ada input yang cukup
        }
    }
}
