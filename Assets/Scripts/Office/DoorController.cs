using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private bool initialToggle;
    [SerializeField]
    private bool isEnable = true; // By default the object is enabled

    private Animator animator;

    private void Awake()
    {
        if (transform.parent.TryGetComponent<Animator>(out animator))
            ToggleDoor(initialToggle);
    }

    public void InteractCheck()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ToggleDoor(!animator.GetBool("WasActivated"));
    }

    public void ToggleDoor(bool isOpen)
    {
        // Check locked
        if (isEnable)
            animator.SetBool("WasActivated", isOpen);
    }
}
