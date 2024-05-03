using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Vector2 velocity;
    public float changeFrequency;

    Rigidbody2D rb;
    bool direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
        InvokeRepeating("ChangeDirection", changeFrequency, changeFrequency);
    }

    private void Update()
    {
        // Constantly change velocity according to what direction the bool is
        if (direction == true)
        {
            rb.velocity = -velocity;
        } else if (direction == false)
        {
            rb.velocity = velocity;
        }
    }

    // Set the bool to the opposite
    private void ChangeDirection()
    {
        direction = !direction;
    }
}
