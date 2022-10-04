using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSystem : MonoBehaviour
{
    private LevelManager levelManager;
    private Ray ray;
    public LayerMask layersToHit;

    private SpriteRenderer background;

    private Vector3 screenBounds;
    private Camera fpsCam;

    void Awake()
    {
        //Cursor.visible = false;
        fpsCam = GameObject.Find("FPS Main Camera").GetComponent<Camera>();

        screenBounds = fpsCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        background = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    void FixedUpdate()
    {
        ray = fpsCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitData, 100, layersToHit))
        {            
            MoveAim(hitData);
        }
    }
    
    void OnMouseDown()
    {
        Ray aimRay = fpsCam.ScreenPointToRay(Input.mousePosition);
        DuckMovement duck;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(aimRay, out RaycastHit hitData, 100, layersToHit))
            {
                if (hitData.transform.tag == "Enemies")
                {
                    duck = hitData.collider.GetComponent<DuckMovement>();

                    Debug.Log($"Duck[{duck}], Destination[{duck.destination}], Speed[{duck.flyingSpeed}]");

                    levelManager.AddHitCount();
                    levelManager.AddScore(Vector3.Distance(transform.position, duck.destination) * duck.flyingSpeed * 10);
                    Destroy(hitData.transform.gameObject);
                    levelManager.CheckGameOver();                    
                }
            }
        }
    }

    void MoveAim(RaycastHit hitData)
    {        
        if (hitData.transform.name == "Background" || hitData.transform.tag == "Enemies")
        {
            transform.position = new Vector3(
                Mathf.Clamp(hitData.point.x, -screenBounds.x, screenBounds.x), 
                Mathf.Clamp(hitData.point.y, -screenBounds.y, screenBounds.y), 
                transform.position.z);
        }   
    }
}
