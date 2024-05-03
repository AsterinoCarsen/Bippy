using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only destroy when a cannon hits ground or a player, if its a player hurt them
        if (collision.transform.tag == "Player")
        {
            collision.GetComponent<PlayerController>().Damage(1, new Vector2(0, 10), 0.1f);
            StartCoroutine(Destroy());
        } else if (collision.transform.tag == "Ground")
        {
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        // Destroy collision to avoid double damage, freeze the object, play animation, wait for animation then destroy
        Destroy(GetComponent<CircleCollider2D>());
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Destroy");
        yield return new WaitForSecondsRealtime(0.6f);
        Destroy(gameObject);
    }
}
