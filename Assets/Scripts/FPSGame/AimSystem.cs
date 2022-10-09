using UnityEngine;

public class AimSystem : MonoBehaviour
{
    private LevelManager levelManager;
    private Ray ray;
    public LayerMask layersToHit;

    private Vector3 screenBounds;
    private Camera fpsCam;

    void Awake()
    {
        // Cursor.visible = false;
        fpsCam = GameObject.Find("FPS Main Camera").GetComponent<Camera>();

        // Convert the size of monitor into scren world points
        screenBounds = fpsCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    void FixedUpdate()
    {
        // Convert the mouse position into ray
        ray = fpsCam.ScreenPointToRay(Input.mousePosition);

        // Simulate a ray line from mouse position to define where is hitting
        if (Physics.Raycast(ray, out RaycastHit hitData, 100, layersToHit))
        {            
            MoveAim(hitData);
        }
    }
    
    // When any mouse button is pressed
    void OnMouseDown()
    {
        Ray aimRay = fpsCam.ScreenPointToRay(Input.mousePosition);
        DuckMovement duck;

        // Primary mouse button key
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Simulate a ray line from mouse position to define where is hitting
            if (Physics.Raycast(aimRay, out RaycastHit hitData, 100, layersToHit))
            {
                // When the hitdata hit an enemy
                if (hitData.transform.tag == "Enemies")
                {
                    duck = hitData.collider.GetComponent<DuckMovement>();

                    // Hit the duck, add score, destroy and check if was the last duck in scene
                    levelManager.AddHitCount();
                    levelManager.AddScore(Vector3.Distance(transform.position, duck.destination) * duck.flyingSpeed * 10);
                    Destroy(hitData.transform.gameObject);
                    levelManager.CheckGameOver();                    
                }
            }
        }
    }

    // Move the aim sprite when is on background or enemy sprite [change this solution in the future]
    void MoveAim(RaycastHit hitData)
    {        
        // Only move when
        if (hitData.transform.name == "Background" || hitData.transform.tag == "Enemies")
        {
            // Clamp into screen bounds
            transform.position = new Vector3(
                Mathf.Clamp(hitData.point.x, -screenBounds.x, screenBounds.x), 
                Mathf.Clamp(hitData.point.y, -screenBounds.y, screenBounds.y), 
                transform.position.z);
        }   
    }
}
