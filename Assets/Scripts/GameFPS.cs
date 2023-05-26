using UnityEngine;

public class GameFPS : MonoBehaviour
{
    public GameObject duck;
    public GameObject aim;
    public GameObject score;
    public GameObject background;

    private Vector2 aimPosition;
    public Vector2 backgroundPosition;

    // Update is called once per frame
    void Update()
    {
        // Convert mouse position to world point
        aimPosition = Camera.current.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        
        // Set the background position
        backgroundPosition = background.transform.position;

        // Only move aim when in screen
        aim.transform.position = aimPosition;            
    }
}
