using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    [SerializeField]
    private GameObject screenPrefab;    
    [SerializeField]
    private AudioSource morseAudio;

    private AudioClip sittingSound;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void Update()
    {
        //ComputerStandUp();
    }

    public void InteractCheck()
    {
        DebugMode.InteractiveInfo("Pressione E para interagir");

        if (Input.GetKeyDown(KeyCode.E))
            StartComputer();
    }

    public void PlaySittingSound()
    {
        audioSource.mute = false;

        if (!audioSource.isPlaying)
            audioSource.Stop();

        audioSource.PlayOneShot(sittingSound); // Opening sfx
    }

    public void ComputerSitting()
    {
        if (!StatesController.isOnComputer)
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlaySittingSound();

                StartComputer();

                StatesController.isOnComputer = true;
            }
    }

    public void StartComputer()
    {
        screenPrefab.SetActive(!screenPrefab.activeSelf);
    }

    public void ComputerStandUp()
    {
        if (StatesController.isOnComputer)
        {
            DebugMode.InteractiveInfo("Pressione Esc para levantar");

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlaySittingSound();

                StatesController.isOnComputer = false;
            }
        }
    }
}
