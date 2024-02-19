using UnityEngine;

public class RoachMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the roach's movement
    //public float boundaryLeft = -10f; // Left boundary of the roach's movement
    //public float boundaryRight = 10f; // Right boundary of the roach's movement
    public GameObject leftPoint;
    public GameObject rightPoint;

    private bool movingRight = false; // Direction of movement
    private Transform currentPoint;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentPoint = leftPoint.transform;
    }
    void Update()
    {
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
    }

    // Flip the roach's direction
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // Flip the roach's orientation
        transform.localScale = theScale;
    }
}
