using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerBar : MonoBehaviour
{
    public void OpenStartMenu(GameObject menuBar)
    {
        menuBar.SetActive(menuBar.activeSelf);
    }
}
