using Fusion;
using UnityEngine;

public class GoombaController : NetworkBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        // bị đạp bởi người chơi , die
        // cham vao nguoi choi, mguoi choi die
        var player  = other.collider.GetComponent<PlayerController>();

        if (player != null)
        {
            var contactPoint = other.GetContact(0);
            if(contactPoint.normal.y > 0.5f)
            {
                Runner.Despawn(Object);
            }
            else
            {
                Debug.Log($"{player.PlayerName} has been defeated by Goomba!");
            }
        }

    }
}
