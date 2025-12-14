using System;
using Fusion;
using UnityEngine;

public class CoinPickup : NetworkBehaviour
{
    public float coinValue = 1f;
    private bool isCollected = false; // Trạng thái đã thu thập

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected) return;
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            isCollected = true; // Đánh dấu đã thu thập
            player.Coins += 1; // Tăng số coin của người chơi
            Runner.Despawn(Object);
            // Gọi RPC để cấu hình coin (ví dụ: thay đổi tên hoặc màu sắc)
            // RPC_Configure("CollectedCoin", Color.yellow);
        }
    }
    
    // [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    // public void RPC_Configure(string name, Color color){
    //     Debug.Log($"Configuring coin with name: {name} and color: {color}");
    // }
}
