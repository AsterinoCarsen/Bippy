using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounding : MonoBehaviour
{
    PlayerController controller;
    // Original, editor selected, move speed
    private float m_playerMoveSpeed;

    void Awake()
    {
        controller = GetComponentInParent<PlayerController>();
        m_playerMoveSpeed = controller.moveSpeed;
    }

    // Reset jumps, dashes and air movement speed
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            controller.isGrounded = true;
            controller.ResetJumps();
            controller.moveSpeed = m_playerMoveSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            controller.isGrounded = false;
            controller.moveSpeed += controller.airMovementMultiplier;
        }
    }
}
