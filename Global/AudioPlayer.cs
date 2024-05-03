using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioSource speaker;

    void Start()
    {
        speaker = GetComponent<AudioSource>();
    }

    // Play the audio clip, but at a slight pitch change
    public void PlaySound(AudioClip sound)
    {
        speaker.pitch = Random.Range(0.95f, 1.05f);
        speaker.PlayOneShot(sound);
    }
}
