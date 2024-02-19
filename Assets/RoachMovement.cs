using UnityEngine;

public class RoachMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the roach's movement
    public float groundCheckDistance;
    //public float boundaryLeft = -10f; // Left boundary of the roach's movement
    //public float boundaryRight = 10f; // Right boundary of the roach's movement
    //public GameObject leftPoint;
    //public GameObject rightPoint;

    private bool movingRight = false; // Direction of movement
    private Vector2 currDir;
    private Transform currentPoint;

    private Rigidbody2D rb;

    public Transform groundDetection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //currentPoint = leftPoint.transform;
        currDir = Vector2.left;
    }
    void Update()
    {
        /*
        Vector2 point = currentPoint.position - transform.position;

        if(currentPoint == leftPoint.transform)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        Debug.Log(Vector2.Distance(transform.position, currentPoint.position));

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == leftPoint.transform)
        {
            currentPoint = rightPoint.transform;
            Debug.Log("MOVING RIGHT");
        }
        else if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == rightPoint.transform)
        {
            currentPoint = leftPoint.transform;
        }
        */

        transform.Translate(currDir * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundCheckDistance);

        Debug.Log(groundInfo.collider);
        Debug.DrawRay(groundDetection.position, Vector2.down * groundCheckDistance, Color.red);
        if(groundInfo.collider == null)
        {
            Flip();
        }
    }

    // Flip the roach's direction
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // Flip the roach's orientation
        transform.localScale = theScale;

        if(currDir == Vector2.left)
        {
            currDir = Vector2.right;
        }
        else
        {
            currDir = Vector2.left;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(groundDetection.position, Vector2.down * groundCheckDistance);
    }
}
