using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugMode : MonoBehaviour
{
    private static TextMeshProUGUI pauseInfo;
    private static TextMeshProUGUI debugInfo;
    private static TextMeshProUGUI interactiveInfo;
    private static GameObject raycastObject;
    private static string additionalInfo;

    public void Start()
    {
        debugInfo = GameObject.Find("Information").GetComponent<TextMeshProUGUI>();
        interactiveInfo = GameObject.Find("InteractiveInfo").GetComponent<TextMeshProUGUI>();
        pauseInfo = GameObject.Find("PauseInfo").GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = (Time.timeScale == 0 ? 1 : 0);

            if (Time.timeScale == 0)
            {
                pauseInfo.text = "Jogo Pausado";
                StatesController.isPaused = true;
            }
            else
            {
                pauseInfo.text = ".";
                StatesController.isPaused = false;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.End))
            Application.Quit();

        if (StatesController.isDebugMode)
        {
            LightSourceController lightClass;
            DoorController doorController;
            Moveable moveable;

            if (raycastObject == null)
                SetDebugText("Debug.Information");
            else
            {
                SetDebugText($"Object[{raycastObject.name}]");
                AddDebugText($"Position[{raycastObject.transform.position}], ");
                AddDebugText($"Quaternion[{raycastObject.transform.rotation}], ");
                AddDebugText($"Light[{raycastObject.TryGetComponent<LightSourceController>(out lightClass)}], ");
                AddDebugText($"Door[{raycastObject.TryGetComponent<DoorController>(out doorController)}], ");
                AddDebugText($"Moveable[{raycastObject.TryGetComponent<Moveable>(out moveable)}], ");
                AddDebugText($"Additional Info[{additionalInfo}]");
            }
        }
    }

    public static void SetAdditionalInfo(string info)
    {
        if (StatesController.isDebugMode)
            additionalInfo = info;
    }

    public static void SetHitObject(GameObject hitObject)
    {
        if (StatesController.isDebugMode)
            raycastObject = hitObject;
    }

    public static void SetDebugText(string text)
    {
        if (StatesController.isDebugMode)
            debugInfo.text = text;
    }

    public static void AddDebugText(string text)
    {
        if (StatesController.isDebugMode)
        {
            if (debugInfo.text == null)
                debugInfo.text = text;
            else
                debugInfo.text += "\n\t" + text;
        }
    }

    public static void ResetText()
    {
        if (StatesController.isDebugMode)
            debugInfo.text = null;
    }

    public static void InteractiveInfo()
    {
        interactiveInfo.text = "";
    }

    public static void InteractiveInfo(string info)
    {
        interactiveInfo.text = info;
    }
}
