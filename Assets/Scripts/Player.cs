using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool onGround;

    [Header("Wall Climbing")]
    public float wallCheckRadius;     //Radius of ground check sphere
    public float slideVelocity;
    public Vector2 sideOffset;       //Offset from player's transform to perform ground checks
    private bool onWall;


    private int masksKilled = 0;

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
    // Start is called before the first frame update
    void Start()
    {
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
        else if(rb.velocity.y > 0 && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)))
        {
            rb.velocity += lowJumpVector * Time.deltaTime;
        }

        if (onGround && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)))
        {
            Jump();
        }
    }

    private void Move(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

        if(dir.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-0.2371941f, 0.2486913f, 1);
        }
        else if(dir.x > 0)
        {
            gameObject.transform.localScale = new Vector3(0.2371941f, 0.2486913f, 1);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
    }

    private void WallSlide()
    {
        rb.velocity = new Vector2(rb.velocity.x, -slideVelocity);
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
}
