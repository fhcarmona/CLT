using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    // Unity variable classes
    public Vector3 destination { get; set; }
    private Vector3 origin;

    // Duck variables
    public float flyingSpeed { get; set; }
    private float destinationBound = 7.2f;
    private float destinationHeight = 7.5f;
    private float originBound = 5.0f;

    // External script variable
    private LevelManager levelManager;
    private AimSystem aimSystem;

    // Start is called before the first frame update
    void Awake()
    {
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        aimSystem = GameObject.Find("Aim").GetComponent<AimSystem>();

        // Generate a new destination
        destination = new Vector3(Random.Range(-destinationBound, destinationBound), destinationHeight, gameObject.transform.position.z);

        // Generate a new origin
        if (destination.x < 0)            
            origin = new Vector3(Random.Range(0, originBound), -5, 0);
        else
            origin = new Vector3(Random.Range(-originBound, 0), -5, 0);

        // Origin
        transform.position = origin;

    }

    // Do physics updates
    void FixedUpdate()
    {
        MoveDuck(gameObject, destination);

        DestroyDuck();
    }

    // Movement the duck to origin to the target
    void MoveDuck(GameObject duck, Vector3 target)
    {
        // Flip the sprite when the duck target position is at left side
        if (target.x < 0)
            duck.gameObject.GetComponent<SpriteRenderer>().flipX = true;

        // Move the duck time a bit to the target position
        duck.transform.Translate(target * Time.fixedDeltaTime * flyingSpeed);
    }

    // Check the duck destroy conditions
    void DestroyDuck()
    {
        // Whenever the duck is out of bounds
        if (gameObject.transform.position.y > destination.y)
        {            
            levelManager.AddMissCount();
            Destroy(gameObject);
            levelManager.CheckGameOver();            
        }
    }

    // When any mouse button is pressed
    void OnMouseDown()
    {
        Debug.Log("Duck Clicked!");

        aimSystem.HitDuck(this);
    }
}