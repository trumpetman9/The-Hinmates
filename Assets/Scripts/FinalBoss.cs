using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public float health;
    private float ori_health;
    public Animator animator;
    public bool hasEvolved;
    public MaskSpawner maskspawner;

    // Start is called before the first frame update
    void Start()
    {
        ori_health = health;
        hasEvolved = false;
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
        if (health < ori_health / 2 && !hasEvolved)
        {
            hasEvolved = true;
            animator.SetTrigger("IsEvolving?");
            maskspawner.spawnInterval = 0.5f;
            maskspawner.maxMasks = 50;
        }
        health = health - x;
        animator.SetTrigger("Hurt");
    }
}





