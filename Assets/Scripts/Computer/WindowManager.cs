using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowManager : MonoBehaviour
{
    public void MinimizeWindow()
    {
        gameObject.SetActive(false);
    }

    public void CloseWindow()
    {
        // Search by taskbar list of open apps
        for (int index = 0; index < TaskBar.taskbarWindows.Count; index++)
            if (TaskBar.taskbarWindows[index].program = gameObject)
            {
                // Destroy app
                Destroy(TaskBar.taskbarWindows[index].taskbarProgram);
                Destroy(TaskBar.taskbarWindows[index].program);

                // Remove app from list
                TaskBar.taskbarWindows.Remove(TaskBar.taskbarWindows[index]);
            }
    }
}
