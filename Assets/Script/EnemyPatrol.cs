using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;       // Tốc độ di chuyển
    [SerializeField] private float patrolDistance = 5f;  // Khoảng cách tối đa từ vị trí bắt đầu

    private Vector3 startingPosition;
    private int direction = 1;  // 1 = sang phải, -1 = sang trái

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        // Tính displacement so với vị trí bắt đầu
        float displacement = transform.position.x - startingPosition.x;

        if (displacement > patrolDistance)
        {
            direction = -1;  // đổi hướng sang trái
        }
        else if (displacement < -patrolDistance)
        {
            direction = 1;   // đổi hướng sang phải
        }

        // Di chuyển enemy
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // Optional: lật sprite khi đổi hướng
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }
}
