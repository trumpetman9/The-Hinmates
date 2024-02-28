using UnityEngine;

public class MaskSpawner : MonoBehaviour
{
    public GameObject maskPrefab; // Assign the mask prefab in the inspector
    public float spawnInterval = 7f; // Time between each spawn

    private float timer;
    public int maxMasks;
    public float spawnOffset = 0f;
    public float yOffset = 0f;
    private int maskTracker;

    void Start()
    {
        timer = spawnInterval; // Initialize the timer
        maskTracker = 0;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0) 
        {
            if (maskTracker < maxMasks) {
                SpawnMask();
                maskTracker += 1;
                timer = spawnInterval; // Reset the timer
            }
        }
    }

    void SpawnMask()
    {
        // Instantiate a new mask prefab at the spawner's position
        Instantiate(maskPrefab, new Vector2(transform.position.x + spawnOffset, transform.position.y + yOffset), Quaternion.identity);
    }
}
