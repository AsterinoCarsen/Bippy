using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpSpeed;
    [Range(0, 20)]
    public float fallSpeed;
    public float airMovementMultiplier;
    [Header("Dash")]
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    [Header("Physics")]
    public float acceleration;
    public float drag;
    [Header("Audio")]
    public AudioClip jump;
    public AudioClip dash;
    [Header("Misc")]
    public int maxHealth;
    public float yKill;

    Rigidbody2D rb;
    bool isRagdoll;
    [HideInInspector]
    public bool isGrounded;
    int jumps = 2;
    Animator anim;
    PlayerAnimator playerAnim;
    SceneChanger sceneChanger;
    AudioPlayer speaker;
    CameraController cameraController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sceneChanger = GameObject.FindWithTag("MainCamera").GetComponent<SceneChanger>();
        playerAnim = GetComponent<PlayerAnimator>();
        speaker = GameObject.FindWithTag("MainCamera").GetComponent<AudioPlayer>();
        anim = GetComponent<Animator>();
        cameraController = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
    }

    void Update()
    {
        // Reduce the fall speed
        if (rb.velocity.y < -fallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -fallSpeed);
        }

        // Move left or right and apply drag when not ragdolling
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (isRagdoll == false)
            {
                Vector2 i = rb.velocity;
                i.x = Mathf.Lerp(i.x, -moveSpeed, acceleration);
                rb.velocity = i;
            }
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (isRagdoll == false)
            {
                Vector2 i = rb.velocity;
                i.x = Mathf.Lerp(i.x, moveSpeed, acceleration);
                rb.velocity = i;
            }
        } else if (isRagdoll == false)
        {
            Vector2 i = rb.velocity;
            i.x = Mathf.Lerp(i.x, 0, drag) * Time.deltaTime;
            rb.velocity = i;
        }

        // Jumping when not ragdolling
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isRagdoll == false && jumps > 0)
            {
                Vector2 i = rb.velocity;
                i.y = jumpSpeed;
                rb.velocity = i;

                jumps -= 1;

                playerAnim.PlayJump();
                speaker.PlaySound(jump);
            }
        }

        // Kill the player if they go below the y kill position
        if (transform.position.y < yKill)
        {
            sceneChanger.NewScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Keep the player from moving for a specific duration and apply a force
    IEnumerator RagDoll(float duration, Vector2 force)
    {
        isRagdoll = true;
        rb.velocity = force;

        yield return new WaitForSeconds(duration);

        isRagdoll = false;
    }

    // Reset jumps back to 2
    public void ResetJumps()
    {
        jumps = 2;
    }
    
    // Damage the player, rag doll them for a certain time and check if the player has died or not
    public void Damage(int amount, Vector2 force, float duration)
    {
        maxHealth -= amount;
        StartCoroutine(RagDoll(duration, force));
        anim.SetBool("Hit", true);
        cameraController.Shake();
        if (maxHealth <= 0)
        {
            sceneChanger.NewScene(SceneManager.GetActiveScene().buildIndex);
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
