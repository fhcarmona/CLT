using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimizedWindow : MonoBehaviour
{
    // Activate the invisible window
    public void OpenWindow()
    {
        int taskbarAppIndex = transform.GetSiblingIndex();

        TaskBar.taskbarWindows[taskbarAppIndex].program.SetActive(true);
    }
}
