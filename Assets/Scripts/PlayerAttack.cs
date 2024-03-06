using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private float timeToAttack;
    public float startTimeAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public LayerMask whatIsWalls;
    public LayerMask whatIsMorty;
    public float attackRange;
    public float damage;

    private bool isAttack;

    private float timeToShove;
    private float startTimeShove;
    public bool shoveEnabled;
    private float timeToRadiusAttack;
    private float startTimeRadiusAttack;
    public bool RadiusAttackEnabled;

    public GameObject forceField;
    [SerializeField] private float forceFieldDuration;
    private float timeToField;
    private float startTimeField;
    

    public Image mb;
    private float timeToManaRegen;
    public float maxMana = 100;
    private float currentMana;

    public AbilityIcon shove;
    public AbilityIcon radiusAttack;


    void Start()
    {
        isAttack = false;
        currentMana = maxMana;
        shoveEnabled = true;
        RadiusAttackEnabled = true;
        startTimeShove = 5;
        startTimeRadiusAttack = 15;
        startTimeField = 5;
        
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
                Collider2D[] mortyToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsMorty);
                Collider2D[] wallsToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsWalls);
                Debug.Log(wallsToDamage);
                for (int i = 0; i < mortyToDamage.Length; i++)
                {
                    Final finalMorty = mortyToDamage[i].GetComponent<Final>();

                    if (finalMorty != null)
                    {
                        finalMorty.TakeDamage(damage);
                    }
                }
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

        if (timeToShove > 0){
            Debug.Log("Shove time left:" +  timeToShove);
            timeToShove -= Time.deltaTime;
        }


        if(Input.GetKey(KeyCode.X) && timeToShove <= 0 && shoveEnabled && currentMana >= 15){
            shove.StartCooldown(startTimeShove);
            SpendMana(15);
            Shove(5f, 20f);  
            timeToShove = startTimeShove;
        }


        if (timeToRadiusAttack > 0){
            Debug.Log("Radius attack left:" + timeToRadiusAttack);
            timeToRadiusAttack -= Time.deltaTime;
        }


        if(Input.GetKey(KeyCode.C) && timeToRadiusAttack <= 0  && RadiusAttackEnabled && currentMana >= 50){
            radiusAttack.StartCooldown(startTimeRadiusAttack);
            SpendMana(50);
            RadiusDamage(10f, 1f);
            timeToRadiusAttack = startTimeRadiusAttack;
        }

        if(timeToField > 0)
        {
            Debug.Log("Force field time left:" + timeToField);
            timeToField -= Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.X) && timeToField <= 0 && currentMana >= 15)
        {
            SpendMana(15);
            GameObject createdField = Instantiate(forceField, transform.position, Quaternion.identity);
            
            timeToField = startTimeField;

            StartCoroutine(DestroyForceField(createdField));
        }


        if (timeToManaRegen > 0)
        {
            timeToManaRegen -= Time.deltaTime;
        }


        if (timeToManaRegen <= 0)
        {
            RegenerateMana(1);
            timeToManaRegen = 0.5f;
        }

        if(currentMana < 15){
            shove.SufficientMana(false);
        } else {
            shove.SufficientMana(true);
        }

        if(currentMana < 50){
            radiusAttack.SufficientMana(false);
        } else {
            radiusAttack.SufficientMana(true);
        }
       
    }

        public void Shove(float radius, float force){
        //idea: for each gameobject enemy, if they are under a straight line distance away from the player, they get shoved away from the player
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemies);

        foreach(Collider2D collider in colliders){

            Rigidbody2D rb = collider.attachedRigidbody;

            //visualize the force field shove jedi thing?


            if(rb != null){
                Vector2 direction = collider.transform.position - transform.position;

                direction.Normalize();

                rb.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }
    }

    
    public void RadiusDamage(float radius, float damage){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemies);

        foreach(Collider2D collider in colliders){
            collider.gameObject.GetComponent<Enemy>().health -= damage;
            collider.gameObject.GetComponent<Enemy>().TurnRed();
        }
    }

    IEnumerator DestroyForceField(GameObject forceField)
    {
        yield return new WaitForSeconds(forceFieldDuration);
        Destroy(forceField);
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Draw the attack range circle in the Scene view when the script is not running
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void SpendMana(int amount){
        currentMana -= amount;
        currentMana = Mathf.Max(0, currentMana);
        mb.fillAmount = (currentMana/maxMana);
        //Debug.Log("Fill amount: " + (currentHealth/maxHealth) * 100);
    }


    public void RegenerateMana(int amount){
        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        mb.fillAmount = (currentMana/maxMana);    
    }

}
