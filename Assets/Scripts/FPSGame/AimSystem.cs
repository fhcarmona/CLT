using UnityEngine;
using UnityEngine.EventSystems;

public class AimSystem : MonoBehaviour
{
    private LevelManager levelManager;

    private float speed = 0.3f;

    void Awake()
    {
        Cursor.visible = false;
        levelManager = GameObject.Find("FPSLevelManager").GetComponent<LevelManager>();
    }

    void FixedUpdate()
    {
        MoveAim();
    }

    // Move the aim
    void MoveAim()
    {
        transform.parent.Translate(new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed, transform.position.z));
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
