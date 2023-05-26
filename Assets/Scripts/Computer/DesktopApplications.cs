using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DesktopApplications : MonoBehaviour
{
    // List of prefab apps
    [SerializeField]
    private GameObject[] apps;

    // List of prefab taskbar apps
    [SerializeField]
    private GameObject[] taskbarApps;

    // Parent to instantiate the objects
    [SerializeField]
    private GameObject taskbarParent, appParent;

    private GameObject cursor;

    private void Start()
    {
        cursor = GameObject.Find("Cursor");

        OpenMessageApp();
    }

    // Open the messenger app
    public void OpenMessageApp()
    {
        int index = 0;

        Window appInstance = TaskBar.GetAppByName(apps[index].name);

        // One per time
        if (appInstance == null)
        {
            GameObject prefabApp = Instantiate(apps[index], appParent.transform);
            GameObject taskbarPrefabApp = Instantiate(taskbarApps[index], taskbarParent.transform);

            // Reset clone name to prefab name
            prefabApp.name = apps[index].name;
            taskbarPrefabApp.name = apps[index].name + "Taskbar";

            // Add the vinculed window-taskbar to the list
            TaskBar.taskbarWindows.Add(new Window(prefabApp, taskbarPrefabApp));

            // Define the taskbar window title
            taskbarPrefabApp.GetComponentInChildren<TextMeshProUGUI>().text = prefabApp.GetComponentInChildren<TextMeshProUGUI>().text;
        }
        else
        {
            Debug.Log($"Activate[{apps[index].name}]");

            appInstance.program.SetActive(true);
            appInstance.taskbarProgram.SetActive(true);
        }

        cursor.transform.SetAsLastSibling();
    }

    // Open the FPS minigame
    public void OpenFPSApp()
    {
        int index = 1;

        // One per time
        if (GameObject.Find(apps[index].name) == null)
        {
            GameObject prefabApp = Instantiate(apps[index], appParent.transform);
            GameObject taskbarPrefabApp = Instantiate(taskbarApps[index], taskbarParent.transform);

            // Reset clone name to prefab name
            prefabApp.name = apps[index].name;
            taskbarPrefabApp.name = apps[index].name + "Taskbar";

            // Add the vinculed window-taskbar to the list
            TaskBar.taskbarWindows.Add(new Window(prefabApp, taskbarPrefabApp));

            // Define the taskbar window title
            taskbarPrefabApp.GetComponentInChildren<TextMeshProUGUI>().text = prefabApp.GetComponentInChildren<TextMeshProUGUI>().text;
        }

        cursor.transform.SetAsLastSibling();
    }

    // Open the contability app
    public void OpenContabilityApp()
    {
        int index = 2;

        // One per time
        if (GameObject.Find(apps[index].name) == null)
        {
            GameObject prefabApp = Instantiate(apps[index], appParent.transform);
            GameObject taskbarPrefabApp = Instantiate(taskbarApps[index], taskbarParent.transform);

            // Reset clone name to prefab name
            prefabApp.name = apps[index].name;
            taskbarPrefabApp.name = apps[index].name + "Taskbar";

            // Add the vinculed window-taskbar to the list
            TaskBar.taskbarWindows.Add(new Window(prefabApp, taskbarPrefabApp));

            // Define the taskbar window title
            taskbarPrefabApp.GetComponentInChildren<TextMeshProUGUI>().text = prefabApp.GetComponentInChildren<TextMeshProUGUI>().text;
        }

        cursor.transform.SetAsLastSibling();
    }

    // Open the care minigame
    public void OpenCareApp()
    {
        int index = 3;

        // One per time
        if (GameObject.Find(apps[index].name) == null)
        {
            GameObject prefabApp = Instantiate(apps[index], appParent.transform);
            GameObject taskbarPrefabApp = Instantiate(taskbarApps[index], taskbarParent.transform);

            // Reset clone name to prefab name
            prefabApp.name = apps[index].name;
            taskbarPrefabApp.name = apps[index].name + "Taskbar";

            // Add the vinculed window-taskbar to the list
            TaskBar.taskbarWindows.Add(new Window(prefabApp, taskbarPrefabApp));

            // Define the taskbar window title
            taskbarPrefabApp.GetComponentInChildren<TextMeshProUGUI>().text = prefabApp.GetComponentInChildren<TextMeshProUGUI>().text;
        }

        cursor.transform.SetAsLastSibling();
    }
}
