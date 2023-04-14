using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleObjects : MonoBehaviour
{
    [SerializeField]
    public float maxRotationDegrees;
    [SerializeField]
    public AxisEnum rotationAxis;
    public enum AxisEnum {x, y, z};

    private bool isOriginalRotation = true;

    public void Interact()
    {
        switch (rotationAxis)
        {
            case AxisEnum.x:
                this.transform.Rotate(new Vector3((isOriginalRotation ? maxRotationDegrees : maxRotationDegrees * -1), 0, 0));
                break;
            case AxisEnum.y:
                this.transform.Rotate(new Vector3(0, (isOriginalRotation ? maxRotationDegrees : maxRotationDegrees * -1), 0));
                break;
            case AxisEnum.z:
                this.transform.Rotate(new Vector3(0, 0, (isOriginalRotation ? maxRotationDegrees : maxRotationDegrees * -1)));
                break;
        }

        isOriginalRotation = !isOriginalRotation;
    }
}
