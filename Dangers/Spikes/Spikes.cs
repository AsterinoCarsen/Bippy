using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spikes : MonoBehaviour
{
    public AudioClip hurt;
    AudioPlayer speaker;
    Tilemap tilemap;

    private void Start()
    {
        speaker = GameObject.FindWithTag("MainCamera").GetComponent<AudioPlayer>();
        tilemap = GetComponent<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Damage the player and give them force for 0.25f seconds
        if (collision.transform.tag == "Player")
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();

            controller.Damage(1, new Vector2(0, 10), 0.1f);

            speaker.PlaySound(hurt);
        }
    }
}
