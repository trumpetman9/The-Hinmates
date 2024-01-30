using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MaskMovement : MonoBehaviour
{
    public float speed = 5f; // Adjust speed as needed
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Make sure your player has the "Player" tag
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
