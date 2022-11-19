using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public static string lastScene;

    public void Awake()
    {
        // Set resolution
        Screen.SetResolution(1024, 768, false);

        // Set the volume
        GetComponent<AudioSource>().volume = Helper.GetPrefByKeyName("MusicVolume") / 100;

        Debug.Log($"Musica[{Helper.GetPrefByKeyName("MusicVolume") / 100}], Efeitos[{Helper.GetPrefByKeyName("SFXVolume") / 100}]");
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
