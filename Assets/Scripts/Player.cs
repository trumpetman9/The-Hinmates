using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float speed;

    [Header("Jump")]
    public float jumpForce;
    public float fallMultiplier;        //Gravity multiplier applied when player falls
    public float lowJumpMultiplier;     //Gravity multiplier applied when jump button is tapped (short jump)
    private Vector2 fallVector;         //Precalculated vector to account for increased gravity during player's fall
    private Vector2 lowJumpVector;      //Precalculated vector to allow player to make short jumps

    [Header("Ground Check")]
    public float groundCheckRadius;     //Radius of ground check sphere
    public Vector2 bottomOffset;       //Offset from player's transform to perform ground checks
    public LayerMask groundLayer;
    [SerializeField] private bool onGround;

    [Header("Wall Climbing")]
    public float wallCheckRadius;     //Radius of ground check sphere
    public float slideVelocity;
    public Vector2 sideOffset;       //Offset from player's transform to perform ground checks
    private bool onWall;

    public bool isFacingRight = true;


    private int masksKilled = 0;

    [Header("Knockback")]
    public float knockbackForce; // Handles Knockback of Player\
    public float knockbackLength;
    public float knockbackDecel;
    public bool knockFromRight;
    [SerializeField] private bool knockedBack;


    [Header("Health")]
    public float maxHealth = 100;
    private float currentHealth;



    public int MasksKilled
    {
        get { return masksKilled; }
        private set { masksKilled = value; } // Keep or remove this
    }



    // Public method to increment the kill count
    public void IncrementKillCount()
    {
        masksKilled++;
        Debug.Log("Masks Killed: " + masksKilled);
    }

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Image hb; // health bar

    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        fallVector = Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1);
        lowJumpVector = Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1);
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        Vector2 inputDir = new Vector2(xInput, yInput);

        Move(inputDir);

        //On ground check
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, groundCheckRadius, groundLayer);
        //On wall check
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + sideOffset, wallCheckRadius, groundLayer) ||
            Physics2D.OverlapCircle((Vector2)transform.position - sideOffset, wallCheckRadius, groundLayer);

        if (onWall && !onGround && rb.velocity.y < 0)
        {
            Debug.Log("Wall Slide");
            WallSlide();
        }

        //Increase gravity by fallMultiplier if player is falling
        if(rb.velocity.y < 0)
        {
            rb.velocity += fallVector * Time.deltaTime;
        }
        //Increase gravity by lowJumpMultiplier if jump button is tapped 
        else if(rb.velocity.y > 0 && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            rb.velocity += lowJumpVector * Time.deltaTime;
        }

        if (onGround && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            Jump();
        }
        if(currentHealth == 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }

    // This function is called when a collision occurs
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a specific tag or layer, if needed
        if (collision.gameObject.CompareTag("Door"))
        {
            // Load the next scene
            SceneManager.LoadScene("Level2");
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(5);
        }
        
    }


    private void Move(Vector2 dir)
    {
        if (!knockedBack)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

            if (isFacingRight && dir.x < 0f || !isFacingRight && dir.x > 0f)
            {
                Vector3 localScale = transform.localScale;
                isFacingRight = !isFacingRight;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, knockbackDecel * Time.deltaTime), rb.velocity.y);
        }
    }
        

    private void Jump()
    {
        if (!knockedBack)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += Vector2.up * jumpForce;
        }
    }

    private void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, -slideVelocity);
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
        while(sr.color != Color.white)
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Ground check sphere
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, groundCheckRadius);
        //Wall check spheres
        Gizmos.DrawWireSphere((Vector2)transform.position + sideOffset, wallCheckRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position - sideOffset, wallCheckRadius);
    }

    public void TakeDamage(int amount){
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);
        hb.fillAmount = (currentHealth/maxHealth);
        //Debug.Log("Fill amount: " + (currentHealth/maxHealth) * 100);
    }

    public void HealDamage(int amount){
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        hb.fillAmount = (currentHealth/maxHealth);        
    }

}
