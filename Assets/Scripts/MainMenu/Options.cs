using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Options : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown resolution;
    [SerializeField]
    private Toggle fullscreen;
    [SerializeField]
    private Slider musicSlider, sfxSlider;

    private int resHeight, resWidth;
    private bool isFullscreen;

    public void Awake()
    {
        LoadOptionsUI();
    }

    public void LoadOptionsUI()
    {
        // Get the saved data
        if (PlayerPrefs.HasKey("Resolution") && resolution != null)
        {
            resolution.SetValueWithoutNotify(PlayerPrefs.GetInt("Resolution"));
        }

        if (PlayerPrefs.HasKey("Fullscreen") && fullscreen != null)
        {
            fullscreen.isOn = PlayerPrefs.GetInt("Fullscreen") == 1 ? true : false;
        }

        if (PlayerPrefs.HasKey("MusicVolume") && musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume"); // When change value, change the text
        }

        if (PlayerPrefs.HasKey("SFXVolume") && sfxSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume"); // When change value, change the text
        }
    }

    public void SaveOptions()
    {
        string[] splitValues;

        try
        {
            // Split the widthxheight value
            splitValues = resolution.captionText.text.Split('x');

            // Convert the window size
            resWidth = Int16.Parse(splitValues[0]);
            resHeight = Int16.Parse(splitValues[1]);
        }
        catch
        {
            // Default value
            resWidth = 800;
            resHeight = 600;
        }

        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetInt("Resolution", resolution.value);
        PlayerPrefs.SetInt("ResolutionWidth", resWidth); // New screen width
        PlayerPrefs.SetInt("ResolutionHeight", resHeight); // New screen height
        PlayerPrefs.SetInt("Fullscreen", fullscreen.isOn ? 1 : 0); // Chance int to boolean
        PlayerPrefs.Save();
    }

    public void SetOptions()
    {
        // Get the saved data
        if (PlayerPrefs.HasKey("Resolution") && PlayerPrefs.HasKey("Fullscreen"))
        {
            resWidth = PlayerPrefs.GetInt("ResolutionWidth");
            resHeight = PlayerPrefs.GetInt("ResolutionHeight");
            isFullscreen = PlayerPrefs.GetInt("Fullscreen") == 1 ? true : false;

            Screen.SetResolution(resWidth, resHeight, isFullscreen);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {

        }
    }

    public void ResetOption()
    {
        PlayerPrefs.SetFloat("SFXVolume", 80); // Default volume
        PlayerPrefs.SetFloat("MusicVolume", 80); // Default volume
        PlayerPrefs.SetInt("Resolution", 2); // 800x600 option
        PlayerPrefs.SetInt("ResolutionWidth", 800); // New screen width
        PlayerPrefs.SetInt("ResolutionHeight", 600); // New screen height
        PlayerPrefs.SetInt("Fullscreen", 0); // Windowed
        PlayerPrefs.Save();

        LoadOptionsUI();
        SetOptions();
    }

    public void SFXSlider()
    {
        TextMeshProUGUI label = sfxSlider.GetComponentInChildren<TextMeshProUGUI>();
        AudioSource sfx = sfxSlider.GetComponent<AudioSource>();

        sfx.Play();
        sfx.volume = sfxSlider.value / 100;
        label.text = sfxSlider.value.ToString();
    }

    public void MusicSlider()
    {
        TextMeshProUGUI label = musicSlider.GetComponentInChildren<TextMeshProUGUI>();
        AudioSource music = musicSlider.GetComponent<AudioSource>();

        music.Play();
        music.volume = musicSlider.value / 100;
        label.text = musicSlider.value.ToString();    
    }
}
