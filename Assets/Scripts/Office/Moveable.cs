using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Rigidbody objPhysics;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        objPhysics = GetComponent<Rigidbody>();
    }

    public void MoveableCheck()
    {
        if (Input.GetKey(KeyCode.Z))
            objPhysics.isKinematic = false;
        else
            objPhysics.isKinematic = true;

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }
    }
}
