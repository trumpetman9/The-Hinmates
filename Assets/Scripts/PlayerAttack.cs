using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeToAttack;
    public float startTimeAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public LayerMask whatIsWalls;
    public float attackRange;
    public float damage;

    private bool isAttack;

    void Start()
    {
        isAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Cooldown logic
        if (timeToAttack > 0)
        {
            timeToAttack -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && timeToAttack <= 0)
        {
            if (!isAttack)
            {
                isAttack = true;

                GetComponentInChildren<Animator>().Play("Slash Animation");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                Collider2D[] wallsToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsWalls);
                Debug.Log(wallsToDamage);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().health -= damage;
                    enemiesToDamage[i].GetComponent<Enemy>().Knockback(transform);

                    Debug.Log("hi1");

                    if (enemiesToDamage[i].GetComponent<Enemy>().health <= 0)
                    {
                        Debug.Log("hi2");
                        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
                        if (playerObject != null)
                        {
                            playerObject.GetComponent<Player>().IncrementKillCount();
                        }
                    }

                }
                for (int i = 0; i < wallsToDamage.Length; i++)
                {
                    wallsToDamage[i].GetComponent<DestroyableWall>().health -= damage;
                }

                // Reset the cooldown timer
                timeToAttack = startTimeAttack;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isAttack = false;
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Draw the attack range circle in the Scene view when the script is not running
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
