using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class OptionsInGame : MonoBehaviour, IPointerExitHandler
{
    [SerializeField]
    private Slider musicSlider, sfxSlider;

    [SerializeField]
    private Image sfxMutedIcon, musicMutedIcon;

    private TextMeshProUGUI sfxLabel, musicLabel;

    public void Awake()
    {
        sfxLabel = sfxSlider.GetComponentInChildren<TextMeshProUGUI>();
        musicLabel = musicSlider.GetComponentInChildren<TextMeshProUGUI>();

        LoadOptionsUI();
    }

    public void LoadOptionsUI()
    {
        // Get the saved data
        if (PlayerPrefs.HasKey("MusicVolume") && musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume"); // When change value, change the text
        }

        if (PlayerPrefs.HasKey("SFXVolume") && sfxSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume"); // When change value, change the text
        }

        SoundIconToggle();
    }

    // When close
    public void SaveOptions()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.Save();
    }

    // Slider function inspector
    public void SFXSlider()
    {
        AudioSource sfx = sfxSlider.GetComponent<AudioSource>();

        sfx.Play();
        sfx.volume = sfxSlider.value / 100;
        sfxLabel.text = (Math.Round(sfxSlider.value)).ToString();

        SoundIconToggle();
    }

    public void MusicSlider()
    {
        AudioSource music = musicSlider.GetComponent<AudioSource>();

        music.Play();
        music.volume = musicSlider.value / 100;
        musicLabel.text = (Math.Round(musicSlider.value)).ToString();

        SoundIconToggle();
    }

    public void SoundIconToggle()
    {
        // Define when show the text or the icon
        if (sfxSlider.value == 0)
        {
            sfxLabel.gameObject.SetActive(false);
            sfxMutedIcon.gameObject.SetActive(true);
        }
        else
        {
            sfxLabel.gameObject.SetActive(true);
            sfxMutedIcon.gameObject.SetActive(false);
        }

        // Define when show the text or the icon
        if (musicSlider.value == 0)
        {
            musicLabel.gameObject.SetActive(false);
            musicMutedIcon.gameObject.SetActive(true);
        }
        else
        {
            musicLabel.gameObject.SetActive(true);
            musicMutedIcon.gameObject.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SaveOptions();

        gameObject.SetActive(false);
    }
}
