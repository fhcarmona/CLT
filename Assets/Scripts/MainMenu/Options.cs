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

    public void Awake()
    {
        LoadOptionsUI();
    }

    public void LoadOptionsUI()
    {
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
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.Save();
    }

    public void SetOptions()
    {
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
        PlayerPrefs.Save();

        LoadOptionsUI();
        SetOptions();
    }

    // Slider function inspector
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
        AudioSource music = GetComponent<AudioSource>();

        music.volume = musicSlider.value / 100;

        label.text = musicSlider.value.ToString();    
    }
}
