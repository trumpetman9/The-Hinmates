using System.Collections;
using UnityEngine;

public class MaskMovement : MonoBehaviour
{
    public float speed = 5f;
    public float pushForce = 10f;
    public float retreatDuration = 1f; // Duration for the mask to move away from the player

    private GameObject player;
    private bool isChasing = true;
    private bool isRetreating = false; // Flag to check if the mask is in retreat mode

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            if (isRetreating)
            {
                RetreatFromPlayer();
            }
            else if (isChasing)
            {
                ChasePlayer();
            }
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void RetreatFromPlayer()
    {
        // Move in the opposite direction of the player
        Vector3 retreatDirection = (transform.position - player.transform.position).normalized;
        transform.position += retreatDirection * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Optionally apply a force to the player
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 pushDirection = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }

            // Start retreating
            if (!isRetreating)
            {
                StartCoroutine(RetreatForDuration(retreatDuration));
            }
        }
    }

    IEnumerator RetreatForDuration(float duration)
    {
        isChasing = false;
        isRetreating = true;
        yield return new WaitForSeconds(duration);
        isRetreating = false;
        isChasing = true;
    }
}
