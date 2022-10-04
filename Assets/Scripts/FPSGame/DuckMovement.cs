using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    public Vector3 destination { get; set; }
    private Vector3 origin;

    public float flyingSpeed { get; set; }
    private float destinationBound = 10.0f;
    private float destinationHeight = 10.0f;
    private float originBound = 5.0f;

    private LevelManager levelManager;

    // Start is called before the first frame update
    void Awake()
    {
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();

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

    void FixedUpdate()
    {
        MoveDuck(gameObject, destination);

        DestroyDuck();
    }

    void MoveDuck(GameObject duck, Vector3 target)
    {
        if (target.x < 0)
            duck.gameObject.GetComponent<SpriteRenderer>().flipX = true;

        duck.transform.Translate(target * Time.fixedDeltaTime * flyingSpeed);
    }
    void DestroyDuck()
    {
        if (gameObject.transform.position.y > destination.y)
        {            
            levelManager.AddMissCount();
            Destroy(gameObject);
            levelManager.CheckGameOver();            
        }
    }
}
