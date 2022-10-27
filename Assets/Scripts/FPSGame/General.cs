using UnityEngine;
using UnityEngine.SceneManagement;

public class General : MonoBehaviour
{
    // Unity classes variables
    private LevelManager levelManager;

    // Every time an object attached with this script
    private void Awake()
    {
        levelManager = GameObject.Find("FPSLevelManager").GetComponent<LevelManager>();
    }

    // Button action for level select
    public void LevelSelect(int difficulty)
    {
        levelManager.StartGame(difficulty);
    }

    // Reload scene
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
