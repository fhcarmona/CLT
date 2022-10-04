using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class General : MonoBehaviour
{
    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    // Button action for level select
    public void LevelSelect(int difficulty)
    {
        levelManager.StartGame(difficulty);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
