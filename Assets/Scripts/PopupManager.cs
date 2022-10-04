using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupManager : MonoBehaviour
{
    // Unity
    public static List<GameObject> openWindows;
    public static float[] minimizedWindowsXPos = { -4.25f, -2.65f, -1.025f, 0.592f, 2.211f, 3.822f };
    public static float focusWindow;

    public GameObject instance;
    public GameObject toolbarInstance;
    public GameObject triggerMovement;
    public GameObject closeButton;
    public GameObject minimizedButton;

    private void Start()
    {
        // Initialize the list
        if (openWindows == null)
            openWindows = new List<GameObject>();

        // Initialize the properties of the popup
        PopupInitiate();

        // Initialize the properties of the popup on the toolbar
        PopupToolbarInitiate();


                    /*// Get the button and parent component
            clickedButton = gameObject;
            parentObject = gameObject.transform.parent.gameObject;

            if (minimizedWindows != null)
            {
                // Create the toolbar window
                cloneInstance = Instantiate(minimizedWindows, new Vector3(0, -1.468f, 0), new Quaternion(0, 0, 0, 0));

                // Add the toolbar window to the list
                openWindows.Add(cloneInstance);

                // Set the particularity of the minimized window
                cloneInstance.GetComponentInChildren<TextMeshProUGUI>().text = parentObject.name;

                // Set the list of every sprite renderer component
                objectList = cloneInstance.GetComponentsInChildren<Component>();

                // Search for the correctly sprite component
                foreach (Component obj in objectList)
                {
                    // Modify the properties of icon gameobject
                    if (obj.name == "Icon")
                    {
                        if (obj.GetType().Name.Equals("SpriteRenderer"))
                        {
                            // Define the minimized window
                            if (parentObject.name == "FPS Game")
                                obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Objects/FPSGameIcon");

                            if (parentObject.name == "Tetris Game")
                                obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Objects/TetrisGameIcon");

                            if (parentObject.name == "Fighting Game")
                                obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Objects/FightGameIcon");
                        }
                        else if (obj.GetType().Name.Equals("Transform"))
                        {
                            obj.GetComponent<Transform>().transform.localScale = new Vector2(0.15f, 0.15f);
                        }
                    }
                }

            }*/
        }

    private void PopupInitiate()
    {
        instance.GetComponentInChildren<TextMeshProUGUI>().text = "A";
    }

    private void PopupToolbarInitiate()
    {
        toolbarInstance.GetComponentInChildren<TextMeshProUGUI>().text = "B";
    }


    private void OnMouseDrag()
    {
        Debug.Log(gameObject.name);

        // Get mouse position
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);

        // Convert mouse axis screen to world axis
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Move the popup
        if (Input.GetMouseButton(0))
            gameObject.transform.position = mousePos;

        // When primary key is pressed
        /*
        if (Input.GetMouseButton(0))
        {
            // Get mouse position
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);

            // Convert mouse axis screen to world axis
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // Popup x axis bounds
            if (mousePos.x > -4.90f && mousePos.x < 5.06f)
            {
                // Popup y axis bounds
                if (mousePos.y > -0.59f && mousePos.y < 4.43f)                
                {
                    // Only when click in the top blue popup background
                    if (clickedButton.name == "MovementTrigger")
                    {
                        // Get the half of parent size
                        //float popupBoundX = parentObject.GetComponent<SpriteRenderer>().bounds.size.x / 2;
                        float popupBoundY = parentObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;

                        // Set the mouse to the top border of the popup
                        mousePos = new Vector3(mousePos.x, mousePos.y - popupBoundY + 0.2f, mousePos.z);

                        // Move the popup
                        parentObject.transform.position = mousePos;
                    }
                }
            }
        }*/
    }
    /*
    void OnMouseDown()
    {
        // When the primary mouse button is pressed
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Minimize popup
            if (clickedButton.name == "MinimizeButton")
            {
                MinimizeWindow(parentObject);
            }
            // Close popup
            else if (clickedButton.name == "CloseButton")
            {
                CloseWindow(parentObject);
            }

            Debug.Log($"{gameObject.name}");
        }
    }

    void LateUpdate()
    {
        // Organize the toolbar opened window
        if (openWindows != null)
            OrganizeToolbar();
    }

    void OrganizeToolbar()
    {
        // Automatic toolbar organizer
        for (int index = 0; index < openWindows.Count; index++)
        {
            openWindows[index].transform.position = new Vector3(minimizedWindowsXPos[index], -1.468f, 0);
        }
    }

    // Custom functions
    void MinimizeWindow(GameObject popup)
    {
        popup.SetActive(false);
    }

    void MaximizeWindow(GameObject minimizedPopup)
    {
        if(!minimizedPopup.activeSelf)
            minimizedPopup.SetActive(true);
    }

    void CloseWindow(GameObject popup)
    {
        // Remove the instance from the list
        openWindows.Remove(cloneInstance);

        // Destroy the instances
        Destroy(popup);
        Destroy(cloneInstance);
    }
    */
}
