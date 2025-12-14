using Fusion;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : NetworkBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;

    public override void Spawned()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
        // Chỉ player có quyền mới được điều khiển
        if (!Object.HasStateAuthority)
            return;

        Move();
        Jump();
    }

    private void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        // Cách đơn giản – đủ dùng cho platformer cơ bản
        return Mathf.Abs(rb.linearVelocity.y) < 0.01f;
    }
}
