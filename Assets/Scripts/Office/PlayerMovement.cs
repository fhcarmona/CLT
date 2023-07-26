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
    private Animator animator;

    private Vector3 velocity = Vector3.zero;

    // FixedUpdate is called once per frame, before update
    void FixedUpdate()
    {
        if(!StatesController.isOnComputer && !StatesController.isPaused)
            Movement();
    }

    // Player simple movement
    private void Movement()
    {
        velocity.x = Input.GetAxis(xAxis) * speed; // Left Right
        velocity.z = Input.GetAxis(zAxis) * speed; // Forward Backward

        // Detect move changes
        if (velocity.x != 0 || velocity.z != 0)
        {
            // Change animation variable
            animator.SetBool("IsMoving", true);

            // Walking sound
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
        }
        else
        {
            // Change animation variable
            animator.SetBool("IsMoving", false);

            // Stop sound
            GetComponent<AudioSource>().Stop();
        }

        // Move the player in the direction and distance of translation
        transform.Translate(velocity * Time.deltaTime);
    }
}
