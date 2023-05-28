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
    }

    // FixedUpdate is called once per frame, before update
    void LateUpdate()
    {
        if (!StatesController.isPaused && !StatesController.isOnComputer)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * interactiveMaxDistance;
            Debug.DrawRay(transform.position, forward, Color.green);

            InteractibleLook();

            MouseLook();

            if (Input.GetKeyDown(KeyCode.F) && StatesController.hasLight && !StatesController.isPaused && !StatesController.isOnComputer)
                ToggleFlashlight();
        }
    }

    // Invert active flashlight and eye adaptation light
    private void ToggleFlashlight()
    {
        foreach (Light lantern in GetComponentsInChildren<Light>(true))
            lantern.gameObject.SetActive(!lantern.gameObject.activeSelf);
    }

    // Verify if flashlight is on
    private bool IsFlashlightOn()
    {
        // Find the active light component (ignore non-active)
        return GetComponentInChildren<Light>().name == "Flashlight";
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

        if (Physics.Raycast(ray, out hit, interactiveMaxDistance, ignoredLayer) && !StatesController.isPaused && !StatesController.isOnComputer)
        {
            DebugMode.SetHitObject(hit.transform.gameObject);

            // Interact classes
            LightSourceController lightController;
            DoorController doorController;
            ComputerController computerController;
            ItemController itemController;
            Moveable moveable;

            PentagramPuzzle pentagramPuzzle;

            if (hit.transform.TryGetComponent<LightSourceController>(out lightController))
            {
                lightController.InteractCheck();
            }
            else if (hit.transform.TryGetComponent<DoorController>(out doorController))
            {
                doorController.InteractCheck();
            }
            else if (hit.transform.TryGetComponent<ComputerController>(out computerController))
            {
                if (IsFlashlightOn() && Input.GetKeyDown(KeyCode.E))
                    ToggleFlashlight();

                computerController.InteractCheck();
            }
            else if (hit.transform.TryGetComponent<ItemController>(out itemController))
            {
                itemController.InteractCheck();
            }
            else if (hit.transform.TryGetComponent<Moveable>(out moveable))
            {
                moveable.InteractCheck();
            }
            else if (hit.transform.parent.parent.TryGetComponent<PentagramPuzzle>(out pentagramPuzzle))
            {
                pentagramPuzzle.SquareCheck(hit.transform.gameObject);
            }
            else
                DebugMode.InteractiveInfo();
        }
        else
        {
            DebugMode.SetHitObject(null);
            DebugMode.InteractiveInfo();
        }
    }
}
