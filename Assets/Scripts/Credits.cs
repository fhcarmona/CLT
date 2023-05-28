using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Credits : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI creditosText;
    private float delayQuit = 0;

    void FixedUpdate()
    {
        delayQuit += Time.deltaTime;

        if (delayQuit > 2)
        {
            creditosText.text = $"OBRIGADO POR JOGAR!";

            if (delayQuit > 10)
                Application.Quit();  
        }
    }
}
