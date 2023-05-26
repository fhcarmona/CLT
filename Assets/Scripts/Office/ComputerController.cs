using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    [SerializeField]
    private GameObject screenPrefab;
    [SerializeField]
    private AudioClip sittingSound;

    private GameObject playerInstance;
    private GameObject computerCamera;
    private AudioSource audioSource;

    private Camera playerCamera;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerInstance = GameObject.Find("Player");
        computerCamera = transform.parent.GetComponentInChildren<Camera>(true).gameObject;

        playerCamera = playerInstance.GetComponentInChildren<Camera>();
    }

    public void Update()
    {
        ComputerStandUp();
    }

    public void InteractCheck()
    {
        DebugMode.InteractiveInfo("Pressione E para sentar");

        ComputerSitting();
    }

    public void ComputerSitting()
    {
        if (Input.GetKeyDown(KeyCode.E) && !StatesController.isOnComputer)
        {
            playerCamera.enabled = false;
            computerCamera.SetActive(true);

            //DebugMode.SetActiveCamera(computerCamera.GetComponentInChildren<Camera>());

            StatesController.isOnComputer = true;

            audioSource.mute = false;

            if (!audioSource.isPlaying)
                audioSource.Stop();

            audioSource.PlayOneShot(sittingSound); // Opening sfx

            // Turn on the monitor
            if (screenPrefab != null)
                screenPrefab.SetActive(true);
        }
    }

    public void ComputerStandUp()
    {
        if (StatesController.isOnComputer)
        {
            DebugMode.InteractiveInfo("Pressione Esc para levantar");

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                computerCamera.SetActive(false);
                playerCamera.enabled = true;

                //DebugMode.SetActiveCamera(playerInstance.GetComponentInChildren<Camera>());

                StatesController.isOnComputer = false;

                audioSource.mute = false;

                if (!audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.PlayOneShot(sittingSound); // Opening sfx
            }
        }
    }
}
