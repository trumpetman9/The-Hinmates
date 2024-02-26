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
    [SerializeField] private bool knockedBack;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            
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
}
