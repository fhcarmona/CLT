using System.Collections;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    // Inspector
    [SerializeField] private GameObject duck;

    private GameObject clone;

    // Execute with delay
    public IEnumerator duckCoroutine(int quantity, float delay, float speed)
    {
        // Spawn the quantity of ducks based on quantity, delay between and speed
        for (int index = 0; index < quantity; index++)
        {
            clone = Instantiate(duck, duck.transform.position, duck.transform.rotation);
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
