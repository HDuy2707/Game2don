using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform pointA;        // Start position
    public Transform pointB;        // End position
    public float speed = 2f;        // Movement speed

    private Animator animator;      // Reference to Animator
    private Vector3 target;         // Current target position

    void Start()
    {
        animator = GetComponent<Animator>();
        target = pointB.position;   // Start moving toward B
    }

    void Update()
    {
        // Move enemy toward target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Flip sprite based on direction
        if (target.x > transform.position.x)
        {
            // Facing right
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (target.x < transform.position.x)
        {
            // Facing left
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Check if reached target
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Switch target
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }

        // Animation control
        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            animator.Play("Run");   // Play Run animation
        }
        else
        {
            animator.Play("Idle");  // Play Idle animation
        }
    }
}
