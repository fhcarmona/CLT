using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class TaskBar : MonoBehaviour
{
    public static List<Window> taskbarWindows;

    [SerializeField]
    private TextMeshProUGUI taskbarTime, taskbarNotification;

    private void Start()
    {
        if (taskbarWindows == null)
            taskbarWindows = new List<Window>();

        StartCoroutine(HourUpdate());
        StartCoroutine(NotificationUpdate());
    }

    // Update the taskbar time every second
    IEnumerator HourUpdate()
    {
        while (true)
        {
            taskbarTime.text = DateTime.Now.ToShortTimeString();
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator NotificationUpdate()
    {
        while (true)
        {
            GameObject msnObj = GameObject.Find("Messenger");
            Messenger messenger;

            int messageCount;

            if (msnObj != null)
            {
                messenger = msnObj.GetComponent<Messenger>();
                messageCount = messenger.CountRemainingMessages();

                if (messageCount != 0)
                    taskbarNotification.text = messageCount.ToString();
                else
                    taskbarNotification.text = "";
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public static Window GetAppByName(string appName)
    {
        foreach (Window window in taskbarWindows)
            if (window.program.name == appName)
                return window;

        return null;
    }
}
