using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeToAttack;
    public float startTimeAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public float damage;


    // Update is called once per frame
    void Update()
    {
        if (timeToAttack <= 0)
        {

            if (Input.GetKey(KeyCode.F))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().health -= damage;
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

            }
            timeToAttack = startTimeAttack;

        }
        else
        {
            timeToAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // Draw the attack range circle in the Scene view when the script is not running
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
