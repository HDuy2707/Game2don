using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public NetworkPrefabRef playerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        // spawn người chơi khi họ tham gia vào mạng
        // kiểm tra người chơi đúng
        if (player == Runner.LocalPlayer)
        {
            // spawn nhân vật người chơi tại
            // vị trí ngẫu nhiên trong phạm vi (-5, 5) trên trục x và y = 0
            var spawnPosition = new Vector3(Random.Range(3f, 10f), 2f, 0f);
            var spawner = Runner.Spawn(
                playerPrefab,  // prefab của người chơi
                spawnPosition,  // vị trí spawn
                Quaternion.identity,  // hướng quay mặc định
                player // liên kết với người chơi
                );
        }
    }
}