using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public static string lastScene;

    private Options optionClass;

    public void Awake()
    {
        optionClass = gameObject.AddComponent<Options>();
        optionClass.SetOptions();
    }

    // MainMenu
    public void NewGame()
    {
        CustomLoadScene("Computer");
    }

    public void Options()
    {
        CustomLoadScene("Options");
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Options
    public void Back()
    {
        CustomLoadScene(lastScene);
    }    

    private void CustomLoadScene(string name)
    {
        // Store the last scene
        lastScene = SceneManager.GetActiveScene().name;

        // Close every scene and open the below scene
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
