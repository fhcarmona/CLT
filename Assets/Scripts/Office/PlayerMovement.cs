using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const string zAxis = "Vertical";
    const string xAxis = "Horizontal";

    [SerializeField]
    private float speed = 2.5f;
    [SerializeField]
    private Animator headBobbingAnim;

    private Vector3 velocity = Vector3.zero;

    // FixedUpdate is called once per frame, before update
    void FixedUpdate()
    {
        velocity.x = Input.GetAxis(xAxis) * speed; // Left Right
        velocity.z = Input.GetAxis(zAxis) * speed; // Forward Backward

        if (velocity.z != 0)
            headBobbingAnim.speed = 1;
        else
            headBobbingAnim.speed = 0;

        transform.Translate(velocity * Time.deltaTime);
        // Movement();
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
