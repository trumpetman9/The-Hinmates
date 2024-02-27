using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public float health;
    public float ori_health;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (ori_health > health)
        {
            animator.SetFloat("Damage", 1);
            ori_health = health;

        }
        animator.SetFloat("Damage", 0);
        if (health <= 0)
        {
            Destroy(gameObject);

        }
    }
}
