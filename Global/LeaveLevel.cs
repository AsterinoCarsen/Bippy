using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveLevel : MonoBehaviour
{
    public AudioClip win;
    public GameObject particleWin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Load the next scene in the build index using SceneChanger
        if (collision.transform.tag == "Player")
        {
            StartCoroutine(StartChange(collision.gameObject));
        }
    }

    // Play animation, freeze player, wait for animation then load next scene in build index
    private IEnumerator StartChange(GameObject player)
    {
        GameObject i = Instantiate(particleWin, transform.position, Quaternion.identity);
        GetComponent<Animator>().SetTrigger("Open");
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GameObject.FindWithTag("MainCamera").GetComponent<AudioPlayer>().PlaySound(win);
        GameObject.FindWithTag("BGM").GetComponent<AudioSource>().Pause();
        yield return new WaitForSecondsRealtime(2);
        // Change this to buildIndex + 1
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameObject.FindWithTag("MainCamera").GetComponent<SceneChanger>().NewScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            GameObject.FindWithTag("MainCamera").GetComponent<SceneChanger>().NewScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
