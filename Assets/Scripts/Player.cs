using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;      
    
    public float jumpForce;             
    public float fallMultiplier;        //Gravity multiplier applied when player falls
    public float lowJumpMultiplier;     //Gravity multiplier applied when jump button is tapped (short jump)
    private Vector2 fallVector;         //Precalculated vector to account for increased gravity during player's fall
    private Vector2 lowJumpVector;      //Precalculated vector to allow player to make short jumps

    public float groundCheckRadius;     //Radius of ground check sphere
    public LayerMask groundLayer;
    public Vector2 bottomOffset;       //Offset from player's transform to perform ground checks
    private bool onGround;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        fallVector = Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1);
        lowJumpVector = Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1);
    }

    // Update is called once per frame
    void Update()
    {
        //On ground check
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, groundCheckRadius, groundLayer);

        //Increase gravity by fallMultiplier if player is falling
        if(rb.velocity.y < 0)
        {
            rb.velocity += fallVector * Time.deltaTime;
        }
        //Increase gravity by lowJumpMultiplier if jump button is tapped 
        else if(rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += lowJumpVector * Time.deltaTime;
        }

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        Vector2 inputDir = new Vector2(xInput, yInput);

        Move(inputDir);

        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Move(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, groundCheckRadius);
    }
}
