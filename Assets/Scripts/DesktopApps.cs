using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DesktopApps : MonoBehaviour
{
    // Unity
    public GameObject windowApp;

    private GameObject appInstance;
    private GameObject clickedApp;

    void Update()
    {
        // Get the button and parent component
        clickedApp = gameObject;
    }

    void OnMouseDown()
    {
        Component[] objectList;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (clickedApp.name == "FPSGameIcon")
            {
                // Only one instance per time
                if (appInstance == null)
                {
                    appInstance = Instantiate(windowApp);

                    // Set the particularity of the window
                    appInstance.GetComponentInChildren<TextMeshProUGUI>().text = "FPS Game";
                    appInstance.name = "FPS Game";

                    // Set the list of every sprite renderer component
                    objectList = appInstance.GetComponentsInChildren<Component>();

                    // Search for the correctly sprite component
                    foreach (Component obj in objectList) {
                        if (obj.name == "Icon") {
                            if (obj.GetType().Name.Equals("SpriteRenderer"))
                            {
                                obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Objects/FPSGameIcon");
                            }
                            else if (obj.GetType().Name.Equals("Transform"))
                            {
                                obj.GetComponent<Transform>().transform.localScale = new Vector2(0.15f, 0.15f);
                            }
                        }                        
                    }
                }
            }
            else if (clickedApp.name == "TetrisGameIcon")
            {
                // Only one instance per time
                if (appInstance == null)
                {
                    appInstance = Instantiate(windowApp);

                    // Set the particularity of the window
                    appInstance.GetComponentInChildren<TextMeshProUGUI>().text = "Tetris Game";
                    appInstance.name = "Tetris Game";

                    // Set the list of every sprite renderer component
                    objectList = appInstance.GetComponentsInChildren<Component>();

                    // Search for the correctly sprite component
                    foreach (Component obj in objectList)
                    {
                        if (obj.name == "Icon")
                        {
                            if (obj.GetType().Name.Equals("SpriteRenderer"))
                            {
                                obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Objects/TetrisGameIcon");
                            }
                            else if (obj.GetType().Name.Equals("Transform"))
                            {
                                obj.GetComponent<Transform>().transform.localScale = new Vector2(0.15f, 0.15f);
                            }
                        }
                    }
                }
            }
            else if (clickedApp.name == "FightGameIcon")
            {
                // Only one instance per time
                if (appInstance == null)
                {
                    appInstance = Instantiate(windowApp);

                    // Set the particularity of the window
                    appInstance.GetComponentInChildren<TextMeshProUGUI>().text = "Fighting Game";
                    appInstance.name = "Fighting Game";

                    // Set the list of every sprite renderer component
                    objectList = appInstance.GetComponentsInChildren<Component>();

                    // Search for the correctly sprite component
                    foreach (Component obj in objectList)
                    {
                        if (obj.name == "Icon")
                        {
                            if (obj.GetType().Name.Equals("SpriteRenderer"))
                            {
                                obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Objects/FightGameIcon");
                            }
                            else if (obj.GetType().Name.Equals("Transform"))
                            {
                                obj.GetComponent<Transform>().transform.localScale = new Vector2(0.15f, 0.15f);
                            }
                        }
                    }
                }
            }
        }
    }
}
