using System.Collections;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    // Inspector
    [SerializeField]
    private GameObject duck, parent;

    private GameObject clone;

    private Vector3 origin;

    // Execute with delay
    public IEnumerator duckCoroutine(int quantity, float delay, float speed)
    {
        // Spawn the quantity of ducks based on quantity, delay between and speed
        for (int index = 0; index < quantity; index++)
        {
            origin = parent.transform.position;
            origin.y -= 5f;

            clone = Instantiate(duck, origin, duck.transform.rotation, parent.transform);
            clone.GetComponent<DuckMovement>().flyingSpeed = speed;

            yield return new WaitForSeconds(delay);
        }
    }

    // Spawn a duck in a random position
    public void SpawnDuck(int quantity, float delay, float speed)
    {
        StartCoroutine(duckCoroutine(quantity, delay, speed));
    }
}
