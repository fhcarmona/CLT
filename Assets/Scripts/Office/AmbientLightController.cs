using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLightController : MonoBehaviour
{
    private Color bloodMoon = new Color(0.4901961f, 0, 0, 1f); // Color(125, 0, 0, 255)
    private Color normalMoon = new Color(0.09803922f, 0.09803922f, 0.09803922f, 1f); // Color(25, 25, 25, 255)

    private Light lightInstance;

    // Start is called before the first frame update
    void Start()
    {
        lightInstance = GetComponent<Light>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            lightInstance.color = lightInstance.color.g == 0 ? normalMoon : bloodMoon;
        }
        
        if (Input.GetKey(KeyCode.K))
        {
            Vector3 rotation = transform.rotation.eulerAngles;

            transform.rotation = Quaternion.Euler(rotation.x, rotation.y + 1, rotation.z);
        }

        DebugMode.SetAdditionalInfo($"Color[{lightInstance.color}], Quaternion[{lightInstance.transform.rotation}]");
    }
}
