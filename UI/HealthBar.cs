using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Animator[] hearts;
    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        hearts = GetComponentsInChildren<Animator>();
        controller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.maxHealth == 3)
        {

        } else if (controller.maxHealth == 2)
        {
            hearts[0].SetTrigger("Die");
        } else if (controller.maxHealth == 1)
        {
            hearts[1].SetTrigger("Die");
        } else if (controller.maxHealth == 0)
        {
            hearts[2].SetTrigger("Die");
        }
    }
}
