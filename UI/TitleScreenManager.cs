using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{

    // Goes to 1 in the build index as the first level
    public void PlayNew()
    {
        GameObject.FindWithTag("MainCamera").GetComponent<SceneChanger>().NewScene(1);
    }

    // Player prefs load
    public void Continue()
    {
        if (PlayerPrefs.GetInt("lastScene") > 0)
        {
            GameObject.FindWithTag("MainCamera").GetComponent<SceneChanger>().NewScene(PlayerPrefs.GetInt("lastScene"));
        }
    }

    // Quits the application
    public void QuitGame()
    {
        Application.Quit();
    }
}
