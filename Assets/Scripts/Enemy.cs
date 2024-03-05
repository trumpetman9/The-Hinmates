using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    [Header("Knockback")]
    public float knockbackForce;
    public float knockbackDecel;
    public float knockbackLength;
    public bool knockedBack;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    
    public bool Dead;

    // Start is called before the first frame update
    void Start()
    {
        Dead = false;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(Die());
            
        }
    }

    public void Knockback(Transform other)
    {
        knockedBack = true;

        Vector2 dir = transform.position - other.position;

        rb.velocity = dir.normalized * knockbackForce;

        sr.color = Color.red;

        StartCoroutine(FadeToWhite());
        StartCoroutine(StopKnockback());
    }

    // non-knockback damage
    public void TurnRed() 
    {
        sr.color = Color.red;
        StartCoroutine(FadeToWhite());
    }

    private IEnumerator FadeToWhite()
    {
        while (sr.color != Color.white)
        {
            yield return null;
            sr.color = Color.Lerp(sr.color, Color.white, knockbackDecel * Time.deltaTime);
        }
    }

    private IEnumerator StopKnockback()
    {
        yield return new WaitForSeconds(knockbackLength);
        knockedBack = false;
    }

    private IEnumerator Die(){
        Dead = true;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
