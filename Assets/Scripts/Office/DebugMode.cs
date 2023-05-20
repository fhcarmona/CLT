using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugMode : MonoBehaviour
{
    private static TextMeshProUGUI debugInfo;
    private static GameObject raycastObject;
    private static string additionalInfo;

    public void Start()
    {
        debugInfo = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Time.timeScale = (Time.timeScale == 0 ? 1 : 0);

        LightSourceController lightClass;
        DoorController doorController;

        if (raycastObject == null)
            SetDebugText("Debug.Information");
        else
        {
            SetDebugText($"Object[{raycastObject.name}]");
            AddDebugText($"Position[{raycastObject.transform.position}], ");
            AddDebugText($"Quaternion[{raycastObject.transform.rotation}], ");
            AddDebugText($"HasLight[{raycastObject.TryGetComponent<LightSourceController>(out lightClass)}], ");
            AddDebugText($"IsDoor[{raycastObject.TryGetComponent<DoorController>(out doorController)}], ");
            AddDebugText($"Additional Info[{additionalInfo}]");
        }
    }

    public static void SetAdditionalInfo(string info)
    {
        additionalInfo = info;
    }

    public static void SetHitObject(GameObject hitObject)
    {
        raycastObject = hitObject;
    }

    public static void SetDebugText(string text)
    {
        debugInfo.text = text;
    }

    public static void AddDebugText(string text)
    {
        if (debugInfo.text == null)
            debugInfo.text = text;
        else
            debugInfo.text += "\n\t" + text;
    }

    public static void ResetText()
    {
        debugInfo.text = null;
    }
}
