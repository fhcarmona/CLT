using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const string zAxis = "Vertical";
    const string xAxis = "Horizontal";

    [SerializeField]
    public float speed = 5;

    private Vector3 velocity = Vector3.zero;

    // FixedUpdate is called once per frame, before update
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        // Running
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= 2f;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            speed /= 2f;

        velocity.x = Input.GetAxis(xAxis) * speed; // Left Right
        velocity.z = Input.GetAxis(zAxis) * speed; // Forward Backward

        transform.Translate(velocity * Time.deltaTime);
    }
}