using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    private static AudioSource clickSound;

    [SerializeField]
    private Camera computerCamera;

    void Awake()
    {
        clickSound = GetComponent<AudioSource>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cursorPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, computerCamera.nearClipPlane);

        cursorPosition.x = Mathf.Clamp(cursorPosition.x, 0, 505) + 252.5f;
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, 0, 290) + 145;

        transform.position = computerCamera.ScreenToWorldPoint(cursorPosition);

        if (Input.GetKeyDown(KeyCode.Mouse0) && StatesController.isOnComputer)
        {
            // Set the volume
            clickSound.volume = Helper.GetPrefByKeyName("SFXVolume") / 100;
            clickSound.Play();
        }
    }
}
