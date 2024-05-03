using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator panel;

    public void NewScene(int buildIndex)
    {
        StartCoroutine(bridge(buildIndex));
    }

    // Play animation, when done change to desired scene
    IEnumerator bridge(int buildIndex)
    {
        panel.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(buildIndex);
    }
}
