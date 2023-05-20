using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float mSensitivity = 1.5f;
    [SerializeField]
    private GameObject flashlight;

    private float xMovement = 0f;
    private float yMovement = 0f;

    private float xBounds = 45f;

    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";

    private float interactiveMaxDistance = 2f;

    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // FixedUpdate is called once per frame, before update
    void LateUpdate()
    {
        if (Time.timeScale != 0)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * interactiveMaxDistance;
            Debug.DrawRay(transform.position, forward, Color.green);

            InteractibleLook();

            MouseLook();

            if (Input.GetKeyDown(KeyCode.F))
                ToggleFlashlight();
        }
    }

    private void ToggleFlashlight()
    {
        foreach (Light lantern in GetComponentsInChildren<Light>(true))
            lantern.gameObject.SetActive(!lantern.gameObject.activeSelf);
    }

    private void MouseLook()
    {
        // Horizontal
        xMovement -= Input.GetAxis(yAxis) * mSensitivity;
        xMovement = Mathf.Clamp(xMovement, -xBounds, xBounds);

        // Vertical
        yMovement += Input.GetAxis(xAxis) * mSensitivity;

        // Body movement
        transform.parent.eulerAngles = new Vector3(0f, yMovement, 0f);

        // Head movement
        transform.eulerAngles = new Vector3(xMovement, yMovement, 0f);

        flashlight.transform.eulerAngles = new Vector3(xMovement, yMovement, 0f);
    }

    private void InteractibleLook()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);

        RaycastHit hit;

        int ignoredLayer =~ LayerMask.GetMask("Player");

        if (Physics.Raycast(ray, out hit, interactiveMaxDistance, ignoredLayer))
        {
            DebugMode.SetHitObject(hit.transform.gameObject);

            // Interact classes
            LightSourceController lightController;
            DoorController doorController;

            if (hit.transform.TryGetComponent<LightSourceController>(out lightController))
            {
                lightController.InteractCheck();
            }

            if (hit.transform.TryGetComponent<DoorController>(out doorController))
            {
                doorController.InteractCheck();
            }

            /*Moveable moveableClass;

            if (Input.GetKeyDown(KeyCode.E))
            {
                InteractibleObjects interactableClass;                

                if (hit.transform.TryGetComponent<InteractibleObjects>(out interactableClass))
                {
                    if (hit.transform.name.Contains("Door") ||
                        // Windows
                        hit.transform.name.Equals("FSL") ||
                        hit.transform.name.Equals("FSR") ||
                        hit.transform.name.Equals("MSL") ||
                        hit.transform.name.Equals("MSR"))
                        interactableClass.ToggleDoor();
                    else if (hit.transform.name.Contains("Light"))
                        interactableClass.ToggleLight();
                }
            }
            
            if (hit.transform.TryGetComponent<Moveable>(out moveableClass))
            {
                moveableClass.MoveableCheck();
            }
            */
        }
        else
            DebugMode.SetHitObject(null);
    }
}
