using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
    }

    public void Options()
    {
        //SceneManager.LoadScene(2);
    }

    public void Back()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
