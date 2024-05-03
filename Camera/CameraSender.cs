using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSender : MonoBehaviour
{
    CameraController controller;

    void Start()
    {
        controller = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
    }

    // Send the camera controller the position of the new bound
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "CameraBound")
        {
            controller.MoveCamera(collision.transform.position);
        }
    }
}
