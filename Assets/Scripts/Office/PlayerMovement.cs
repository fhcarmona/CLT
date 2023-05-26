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

    private void Movement()
    {
        velocity.x = Input.GetAxis(xAxis) * speed; // Left Right
        velocity.z = Input.GetAxis(zAxis) * speed; // Forward Backward

        if (velocity.x != 0 || velocity.z != 0)
        {
            animator.SetBool("IsMoving", true);

            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
        }
        else
        {
            animator.SetBool("IsMoving", false);
            GetComponent<AudioSource>().Stop();
        }

        transform.Translate(velocity * Time.deltaTime);
    }
}
