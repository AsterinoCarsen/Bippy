using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float xStart, yStart;
    public int columnLength, rowLength;
    public float xSpace, ySpace;
    [Header("Settings")]
    public float moveSpeed;
    [Header("Misc")]
    public GameObject gridInstance;
    public Transform parent;

    private Vector2 nextPos;
    Animator anim;

    void Start()
    {
        // Generates a grid of objects that the camera uses to define which screen it's on
        for (int i = 0; i < columnLength * rowLength; i++)
        {
            Instantiate(gridInstance, new Vector3(xStart + (xSpace * (i %columnLength)), yStart + (ySpace * (i / columnLength)), -10), Quaternion.identity, parent);
        }
        anim = GetComponent<Animator>();
    }

    // Changes position of the camera using lerp constantly when a new position is fed in
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(nextPos.x, nextPos.y, -10), moveSpeed);
    }

    // Give the new position
    public void MoveCamera(Vector2 newPos)
    {
        nextPos = newPos;
    }

    public void Shake()
    {
        anim.SetTrigger("Shake");
    }
}
