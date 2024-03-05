using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public float health;
    private float ori_health;
    public Animator animator;
    private bool hasEvolved;
    public MaskSpawner maskspawner;
    private bool isEvolving;

    // Start is called before the first frame update
    void Start()
    {
        ori_health = health;
        hasEvolved = false;
        isEvolving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Evolve()
    {
        // Play the animation for getting suck in
        animator.SetTrigger("IsEvolving?");
        isEvolving = true;

        yield return new WaitForSeconds(4);

        // Move this object somewhere off the screen

    }

    public void TakeDamage(float x)
    {
        if (health < ori_health / 2 && !hasEvolved)
        {
            hasEvolved = true;
            StartCoroutine(Evolve());
            maskspawner.spawnInterval = 0.5f;
            maskspawner.maxMasks = 50;
            isEvolving = false;
        }
        if (!isEvolving)
        {
            health = health - x;
            animator.SetTrigger("Hurt");
        }

    }
}





