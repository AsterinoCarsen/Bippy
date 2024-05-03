using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Particle Effects")]
    public GameObject jumpEffect;
    public GameObject runEffect;
    public GameObject dashEffect;

    Rigidbody2D rb;
    Animator anim;
    PlayerController controller;
    SpriteRenderer spr;

    bool isFacingLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Which direction the player wants to go is where they're facing
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            isFacingLeft = true;
            // Play run particles if the player is grounded
            if (controller.isGrounded == true)
            {
                PlayRun();
            }
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            isFacingLeft = false;
            // Play run particles if the player is grounded
            if (controller.isGrounded == true)
            {
                PlayRun();
            }
        }

        if (Mathf.Round(Mathf.Abs(rb.velocity.x)) == 0)
        {
            anim.SetBool("isStatic", true);
        } else if (Mathf.Round(Mathf.Abs(rb.velocity.x)) > 0)
        {
            anim.SetBool("isStatic", false);
        }

        anim.SetBool("isGrounded", controller.isGrounded);


    }

    public void StopHitAnim()
    {
        anim.SetBool("Hit", false);
    }

    public void PlayRun()
    {
        // Find which direction the player is facing and spawn the particle accordinly
        if (isFacingLeft == true)
        {
            GameObject i = Instantiate(runEffect, transform.position, Quaternion.identity);
            i.transform.localScale = new Vector3(Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f), 1);

            i.GetComponent<SpriteRenderer>().flipX = true;
            Destroy(i, 0.4f);
        } else if (isFacingLeft == false)
        {
            GameObject i = Instantiate(runEffect, transform.position, Quaternion.identity);
            i.transform.localScale = new Vector3(Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f), 1);

            Destroy(i, 0.4f);
        }
    }

    public void PlayJump()
    {
        GameObject i = Instantiate(jumpEffect, transform.position, Quaternion.identity);
        i.transform.localScale = new Vector3(Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f), 1);

        Destroy(i, 0.5f);
    }

    public void PlayDash()
    {
        GameObject i = Instantiate(dashEffect, transform.position, Quaternion.identity);

        if (rb.velocity.x > 0)
        {
            i.transform.rotation = new Quaternion(0, 90, 0, 0);
        } else if (rb.velocity.x < 0)
        {
            i.transform.rotation = new Quaternion(0, -90, 0, 0);
        }

        Destroy(i, 1);
    }
}
