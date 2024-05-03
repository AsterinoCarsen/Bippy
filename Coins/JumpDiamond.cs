using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDiamond : MonoBehaviour
{
    public AudioClip collect;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            // If the player collides, reset their jumps, play animation and start respawn
            collision.gameObject.GetComponent<PlayerController>().ResetJumps();
            GameObject.FindWithTag("MainCamera").GetComponent<AudioPlayer>().PlaySound(collect);
            anim.SetTrigger("Die");
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        // Wait for the die animation to play
        yield return new WaitForSeconds(0.4f);

        // Disable the game object without destroying it
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;

        // Delay to respawn
        yield return new WaitForSeconds(3);

        // Reset trigger and re-enable the disable
        anim.ResetTrigger("Die");
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
}
