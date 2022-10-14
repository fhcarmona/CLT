using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Open the messenger app
    public void OpenMessageApp()
    {
        int index = 0;

        // One per time
        if (GameObject.Find(apps[index].name) == null)
        {
            GameObject prefabApp = Instantiate(apps[index], appParent.transform);
            GameObject taskbarPrefabApp = Instantiate(taskbarApps[index], taskbarParent.transform);

            // Reset clone name to prefab name
            prefabApp.name = apps[index].name;
            taskbarPrefabApp.name = taskbarApps[index].name;

            // Add the vinculed window-taskbar to the list
            TaskBar.taskbarWindows.Add(new Window(prefabApp, taskbarPrefabApp));
        }
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
            taskbarPrefabApp.name = taskbarApps[index].name;

            // Add the vinculed window-taskbar to the list
            TaskBar.taskbarWindows.Add(new Window(prefabApp, taskbarPrefabApp));
        }
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
            taskbarPrefabApp.name = taskbarApps[index].name;

            // Add the vinculed window-taskbar to the list
            TaskBar.taskbarWindows.Add(new Window(prefabApp, taskbarPrefabApp));
        }
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
            taskbarPrefabApp.name = taskbarApps[index].name;

            // Add the vinculed window-taskbar to the list
            TaskBar.taskbarWindows.Add(new Window(prefabApp, taskbarPrefabApp));
        }
    }
}
