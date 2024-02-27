using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float ori_health;

    // Start is called before the first frame update
    void Start()
    {
        ori_health = health;



    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float x)
    {
        health = health - x;
    }
}


