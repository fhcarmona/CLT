using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private bool initialToggle;
    [SerializeField]
    private bool isEnable = true; // By default the object is enabled
    [SerializeField]
    private AudioClip[] audioList;

    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (transform.parent.TryGetComponent<Animator>(out animator))
            ToggleDoor(initialToggle);
    }

    public void InteractCheck()
    {
        DebugMode.InteractiveInfo("Pressione E para interagir");

        if (Input.GetKeyDown(KeyCode.E))
        {
            audioSource.mute = false;
            ToggleDoor(!animator.GetBool("WasActivated"));
        }
    }

    public void ToggleDoor(bool isOpen)
    {
        // Check locked
        if (isEnable)
        {
            animator.SetBool("WasActivated", isOpen);

            if (isOpen)
            {
                if (!audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.PlayOneShot(audioList[0]); // Opening sfx
            }
            else
            {
                if (!audioSource.isPlaying)
                    audioSource.Stop();

                audioSource.PlayOneShot(audioList[1]); // Closing sfx
            }
        }
    }
}
