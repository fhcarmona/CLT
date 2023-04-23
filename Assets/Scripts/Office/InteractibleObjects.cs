using UnityEngine;

public class InteractibleObjects : MonoBehaviour
{
    [SerializeField]
    private bool initialToggle;
    [SerializeField]
    private bool isEnable = true; // By default the object is enabled
    [SerializeField]
    private GameObject[] lightObjectList; 

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

            foreach (GameObject lightObject in lightObjectList)
            {
                Light[] lightList = lightObject.GetComponentsInChildren<Light>();

                foreach (Light lightInstance in lightList)
                {
                    if(isOn)
                        lightInstance.intensity = 1;
                    else
                        lightInstance.intensity = 0;
                }
            }
        }
    }
}
