using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public void InteractCheck()
    {
        DebugMode.InteractiveInfo("Pressione E para pegar");

        if (Input.GetKeyDown(KeyCode.E))
        {
            StatesController.hasLight = true;
            Destroy(gameObject);
        }
    }
}
