using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinGet;
    public GameObject indicator;

    AudioPlayer speaker;

    private void Start()
    {
        speaker = GameObject.FindWithTag("MainCamera").GetComponent<AudioPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Play animation and die after it's finished adding it's specific value
        if (collision.transform.tag == "Player")
        {
            // Spawn the text and give it its attributes
            GameObject i = Instantiate(indicator, transform.position + new Vector3(0, 0.5f), Quaternion.identity, GameObject.FindWithTag("WS").transform);
            i.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1);
            Destroy(i, 1f);

            GetComponent<Animator>().SetTrigger("Die");
            GetComponentInParent<CoinManager>().Add(1);
            speaker.PlaySound(coinGet);
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            Destroy(gameObject, 0.4f);
        }
    }
}
