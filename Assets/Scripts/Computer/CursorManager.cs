using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    private static AudioSource clickSound;

    void Awake()
    {
        clickSound = GetComponent<AudioSource>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Set the volume
            clickSound.volume = Helper.GetPrefByKeyName("SFXVolume") / 100;
            clickSound.Play();
        }
    }
}
