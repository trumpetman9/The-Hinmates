using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Ground Check")]
    public float groundCheckRadius;     //Radius of ground check sphere
    public Vector2 bottomOffset;       //Offset from player's transform to perform ground checks
    public LayerMask groundLayer;
    public bool onGround { get; set; }

    [Header("Wall Climbing")]
    public float wallCheckRadius;     //Radius of ground check sphere
    public float slideVelocity;
    public Vector2 sideOffset;       //Offset from player's transform to perform ground checks
    public bool onWall { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //On ground check
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, groundCheckRadius, groundLayer);
        //On wall check
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + sideOffset, wallCheckRadius, groundLayer) ||
            Physics2D.OverlapCircle((Vector2)transform.position - sideOffset, wallCheckRadius, groundLayer);

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
