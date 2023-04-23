using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugMode : MonoBehaviour
{
    private static TextMeshProUGUI debugInfo;
    private static GameObject raycastObject;

    public void Start()
    {
        debugInfo = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Update()
    {
        InteractibleObjects interactClass;
        Moveable moveableClass;

        if (raycastObject == null)
            SetDebugText("Debug.Information");
        else
        {
            SetDebugText($"Object[{raycastObject.name}]");
            AddDebugText($"Position[{raycastObject.transform.position}], ");
            AddDebugText($"Quaternion[{raycastObject.transform.rotation}], ");
            AddDebugText($"Interactable[{raycastObject.TryGetComponent<InteractibleObjects>(out interactClass)}], ");
            AddDebugText($"Moveable[{raycastObject.TryGetComponent<Moveable>(out moveableClass)}]");
        }
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
