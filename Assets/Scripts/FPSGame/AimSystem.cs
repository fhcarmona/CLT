using UnityEngine;
using UnityEngine.EventSystems;

public class AimSystem : MonoBehaviour
{
    private LevelManager levelManager;
    private AudioSource shotSound;

    private float speed = 15;

    void Awake()
    {
        Cursor.visible = false;
        shotSound = GetComponent<AudioSource>();
        levelManager = GameObject.Find("FPSLevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        MoveAim();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Set the volume
            shotSound.volume = Helper.GetPrefByKeyName("SFXVolume") / 100;
            shotSound.Play();
        }
    }

    // Move the aim
    void MoveAim()
    {
        transform.parent.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, transform.position.z));
    }

    // Duck hit detection
    public void HitDuck(DuckMovement duck)
    {
        // Hit the duck, add score, destroy and check if was the last duck in scene
        levelManager.AddHitCount();
        levelManager.AddScore(Vector3.Distance(transform.position, duck.destination) * duck.flyingSpeed * 10);
        Destroy(duck.gameObject);
        levelManager.CheckGameOver();
    }

    // When the aim is over the duck
    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space))
            if (collision.tag == "Enemies")
                HitDuck(collision.GetComponent<DuckMovement>());
    }
}
