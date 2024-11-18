using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiiltController : MonoBehaviour
{
    Rigidbody2D rb;
    float dx;
    float movementSpeed = 20f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dx = Input.acceleration.x * movementSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -7.5f, -7.5f), transform.position.y);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dx, 0f);
    }
}
