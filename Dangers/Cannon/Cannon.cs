using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject cannonBall;
    public float cannonBallSpeed;
    public bool isFacingRight;

    public void Shoot()
    {
        // Change where the cannon ball spawns depending on which direction the cannon is facing
        if (isFacingRight == true)
        {
            GameObject i = Instantiate(cannonBall, transform.position + new Vector3(0.6f, 0), Quaternion.identity);
            i.GetComponent<Rigidbody2D>().velocity = new Vector2(cannonBallSpeed, 0);
        } else if (isFacingRight == false)
        {
            GameObject i = Instantiate(cannonBall, transform.position + new Vector3(-0.6f, 0), Quaternion.identity);
            i.GetComponent<Rigidbody2D>().velocity = new Vector2(-cannonBallSpeed, 0);
        }
    }
}
