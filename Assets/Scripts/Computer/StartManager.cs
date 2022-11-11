using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startMenu, soundMenu;

    // Taskbar functions
    public void ToggleStartMenu()
    {
        startMenu.SetActive(!startMenu.activeSelf);        
    }

    public void ToggleSoundMenu()
    {
        soundMenu.SetActive(!soundMenu.activeSelf);
    }

    // Menubar system
    public void ShutDown()
    {
        Application.Quit();
    }

    public void Logoff()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Configuration()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("Options", LoadSceneMode.Additive);
    }
}