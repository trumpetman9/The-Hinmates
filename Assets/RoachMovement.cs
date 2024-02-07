using UnityEngine;

public class RoachMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the roach's movement
    public float boundaryLeft = -10f; // Left boundary of the roach's movement
    public float boundaryRight = 10f; // Right boundary of the roach's movement

    private bool movingRight = false; // Direction of movement
   

    void Update()
    {
        // Move the roach
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        // Check if the roach has reached the right boundary
        if (transform.position.x >= boundaryRight)
        {
            movingRight = false;
            // Optional: Flip the roach to face the left side
            Flip();
        }
        // Check if the roach has reached the left boundary
        else if (transform.position.x <= boundaryLeft)
        {
            movingRight = true;
            // Optional: Flip the roach to face the right side
            Flip();
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
