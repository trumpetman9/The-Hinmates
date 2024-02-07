using UnityEngine;

public class MaskSpawner : MonoBehaviour
{
    public GameObject maskPrefab; // Assign the mask prefab in the inspector
    public float spawnInterval = 7f; // Time between each spawn

    private float timer;

    void Start()
    {
        timer = spawnInterval; // Initialize the timer
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnMask();
            timer = spawnInterval; // Reset the timer
        }
    }

    void SpawnMask()
    {
        // Instantiate a new mask prefab at the spawner's position
        Instantiate(maskPrefab, transform.position, Quaternion.identity);
    }
}
