using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class TaskBar : MonoBehaviour
{
    public static List<Window> taskbarWindows;

    [SerializeField]
    private TextMeshProUGUI taskbarTime;

    private GameObject window;

    private void Start()
    {
        if (taskbarWindows == null)
            taskbarWindows = new List<Window>();

        StartCoroutine(HourUpdate());
    }

    // Check opened taskbar window
    IEnumerator TaskbarWindows()
    {
        yield return new WaitForSeconds(Time.deltaTime);
    }

    // Update the taskbar time every second
    IEnumerator HourUpdate()
    {
        taskbarTime.text = DateTime.Now.ToShortTimeString();
        yield return new WaitForSeconds(Time.deltaTime);
    }

    public static int AppCountByName(string appName)
    {
        int count = 0;

        foreach (Window window in taskbarWindows)
            if (window.program.name == appName)
                count++;

        return count;
    }
}
