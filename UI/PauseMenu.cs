using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool isPaused = false;
    AudioSource BGM;
    AudioSource SFX;
    Slider SFXVol;
    Slider BGMVol;
    Transform camera;

    private void Start()
    {
        BGM = GameObject.FindWithTag("BGM").GetComponent<AudioSource>();
        SFX = GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>();
        SFXVol = gameObject.transform.Find("SFX").GetComponent<Slider>();
        BGMVol = gameObject.transform.Find("BGM").GetComponent<Slider>();
        camera = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            BGM.volume = BGMVol.value;
            SFX.volume = SFXVol.value;
        }

        if (PlayerPrefs.GetFloat("SFXVol") == 0 || PlayerPrefs.GetFloat("BGMVol") == 0)
        {
            PlayerPrefs.SetFloat("SFXVol", 0.5f);
            PlayerPrefs.SetFloat("BGMVol", 0.5f);
        }

        SFXVol.value = PlayerPrefs.GetFloat("SFXVol");
        BGMVol.value = PlayerPrefs.GetFloat("BGMVol");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(camera.position.x, camera.position.y, 0);

        // Use the volume knob to control volume of BGM and SFX
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            BGM.volume = BGMVol.value;
            SFX.volume = SFXVol.value;
        }

        // Time scale and enabling
        if (isPaused == true)
        {
            Time.timeScale = 0;
            GetComponent<Canvas>().enabled = true;
        } else if (isPaused == false)
        {
            Time.timeScale = 1;
            GetComponent<Canvas>().enabled = false;
        }

        // Single input true and false triggers
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused == true)
            {
                BGM.Pause();
                SFXVol.interactable = true;
                BGMVol.interactable = true;
            } else if (isPaused == false)
            {
                BGM.UnPause();
                SFXVol.interactable = false;
                BGMVol.interactable = false;
            }
        }
    }

    // Quits game
    public void QuitGame()
    {
        isPaused = false;
        PlayerPrefs.SetInt("lastScene", SceneManager.GetActiveScene().buildIndex);
        GameObject.FindWithTag("MainCamera").GetComponent<SceneChanger>().NewScene(0);
    }

    // Save the volume knob when a scene closes
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("SFXVol", SFXVol.value);
        PlayerPrefs.SetFloat("BGMVol", BGMVol.value);
    }
}
