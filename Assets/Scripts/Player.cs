using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float fallMultiplier;
    public float lowJumpMultiplier;

    private Vector2 fallVector;
    private Vector2 lowJumpVector;

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
        if(rb.velocity.y < 0)
        {
            rb.velocity += fallVector * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += lowJumpVector * Time.deltaTime;
        }

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        Vector2 inputDir = new Vector2(xInput, yInput);

        Move(inputDir);

        if (Input.GetKeyDown(KeyCode.Space))
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
}
