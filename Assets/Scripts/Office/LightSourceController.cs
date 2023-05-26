using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceController : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private bool initialToggle;
    [SerializeField]
    private GameObject[] prefabLight;
    [SerializeField]
    private Material lightOnMaterial;
    [SerializeField]
    private Material lightOffMaterial;
    [SerializeField]
    private AudioClip[] audioList;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (transform.parent.TryGetComponent<Animator>(out animator))
            ToggleLight(initialToggle);
    }

    public void InteractCheck()
    {
        DebugMode.InteractiveInfo("Pressione E para interagir");

        if (Input.GetKeyDown(KeyCode.E))
        {
            audioSource.mute = false;
            ToggleLight(!animator.GetBool("WasActivated"));
        }
    }

    public void ToggleLight(bool lightStatus)
    {
        // Animation
        animator.SetBool("WasActivated", lightStatus);

        // Sound
        if (lightStatus)
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

        foreach (GameObject prefab in prefabLight)
        {
            Light[] lightList = prefab.GetComponentsInChildren<Light>();
            MeshRenderer[] meshList = prefab.GetComponentsInChildren<MeshRenderer>();

            // Toggle every light component in prefab
            foreach (Light light in lightList)
                light.enabled = lightStatus;

            // Toggle every bright material in prefab
            foreach (MeshRenderer meshRenderer in meshList)
            {
                // On to off
                if (meshRenderer.material.name.Contains(lightOnMaterial.name) && !lightStatus)
                    meshRenderer.material = lightOffMaterial;
                // Off to on
                else if (meshRenderer.material.name.Contains(lightOffMaterial.name) && lightStatus)
                    meshRenderer.material = lightOnMaterial;
            }
        }
    }
}
