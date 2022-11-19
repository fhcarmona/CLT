using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    // Unity variable classes
    public Vector3 destination { get; set; }
    private Vector3 origin;
    private AudioSource duckSound;

    // Duck variables
    public float flyingSpeed { get; set; }
    private float destinationBound = 7.2f;
    private float destinationHeight = 10;

    // External script variable
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Awake()
    {
        levelManager = GameObject.Find("FPSLevelManager").GetComponent<LevelManager>();
        duckSound = GetComponent<AudioSource>();

        // Generate a new destination
        destination = new Vector3(Random.Range(-destinationBound, destinationBound), destinationHeight, gameObject.transform.position.z);
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
        if (gameObject.transform.position.y > 164)
        {
            // Set the volume
            duckSound.volume = Helper.GetPrefByKeyName("SFXVolume") / 100;
            duckSound.Play();

            levelManager.AddMissCount();
            Destroy(gameObject);
            levelManager.CheckGameOver();
        }
    }
}
