using UnityEngine;
using UnityEngine.EventSystems;

public class AimSystem : MonoBehaviour
{
    private LevelManager levelManager;
    private Ray ray;
    public LayerMask layersToHit;

    private Vector3 screenBounds;
    private Camera fpsCam;

    void Awake()
    {
        Cursor.visible = false;
        fpsCam = GameObject.Find("FPS Main Camera").GetComponent<Camera>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();

        // Convert the size of monitor into scren world points
        screenBounds = fpsCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    void FixedUpdate()
    {
        MoveAim();
    }

    // Move the aim
    void MoveAim()
    {                
        Vector3 mousePos = fpsCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        // Clamp into screen bounds
        transform.position = new Vector3(
            Mathf.Clamp(mousePos.x, -screenBounds.x, screenBounds.x), 
            Mathf.Clamp(mousePos.y, -screenBounds.y, screenBounds.y), 
            transform.position.z);
    }

    // Duck hit detection
    public void HitDuck(DuckMovement duck)
    {
        BoxCollider2D duckCollider = duck.GetComponent<BoxCollider2D>();

        // Check if the aim position is in duck position
        if (duck.tag == "Enemies" && duckCollider.bounds.Contains(transform.position))
        {
            // Hit the duck, add score, destroy and check if was the last duck in scene
            levelManager.AddHitCount();
            levelManager.AddScore(Vector3.Distance(transform.position, duck.destination) * duck.flyingSpeed * 10);
            Destroy(duck.gameObject);
            levelManager.CheckGameOver();
        }
    }
}
