using UnityEngine;

public class InteractibleObjects : MonoBehaviour
{
    [SerializeField]
    private bool initialToggle;
    [SerializeField]
    private bool isEnable = true; // By default the object is enabled
    [SerializeField]
    private Light[] lightList; 

    private Animator animator;

    private void Awake()
    {
        if (transform.parent.TryGetComponent<Animator>(out animator))
        {
            ToggleDoor(initialToggle);
            ToggleLight(initialToggle);
        }
    }

    public void ToggleDoor()
    {
        ToggleDoor(!animator.GetBool("WasActivated"));
    }

    public void ToggleDoor(bool isOpen)
    {
        // Check locked
        if (isEnable)
        {
            animator.SetBool("WasActivated", isOpen);
        }
    }

    public void ToggleLight()
    {
        ToggleLight(!animator.GetBool("WasActivated"));
    }

    public void ToggleLight(bool isOn)
    {
        // Check locked
        if (isEnable)
        {
            animator.SetBool("WasActivated", isOn);

            foreach (Light lightInstance in lightList)
            {
                lightInstance.gameObject.SetActive(isOn);
            }
        }
    }
}
