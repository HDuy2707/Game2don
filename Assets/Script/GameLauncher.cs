using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameLauncher : MonoBehaviour, INetworkRunnerCallbacks
{
    public TMP_InputField playerNameInput;
    public Button playGameButton;
    
    public NetworkRunner networkRunner;
    
    // prefab cua người chơi để spawn trong mạng
    public NetworkPrefabRef playerPrefab;
    public NetworkPrefabRef coinPrefab;
    
    public NetworkPrefabRef goombaPrefab;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playGameButton.onClick.AddListener(ConnectToGame);
    }

    void ConnectToGame()
    {
        // đọc tên người chơi từ input field, nếu trống thì tạo tên ngẫu nhiên
        var playerName = string.IsNullOrEmpty(playerNameInput.text) ?
            "Player" + Random.Range(1000,100000) : playerNameInput.text;
        // lưu tên người chơi vào PlayerPrefs để sử dụng sau này
        PlayerPrefs.SetString("PlayerName", playerName);
        // vô hiệu hóa input field và button để
        // tránh người chơi thay đổi tên hoặc nhấn nhiều lần
        playerNameInput.interactable = false;
        playGameButton.interactable = false;
        // bắt đầu kết nối vào game
        StartGame(GameMode.Shared);
    }
    
    async void StartGame(GameMode mode)
    {
        // tạo NetworkRunner nếu chưa có
        networkRunner = gameObject.AddComponent<NetworkRunner>();
        // thiết lập callback cho NetworkRunner
        networkRunner.ProvideInput = true;
        
        var playerSpawner = gameObject.AddComponent<PlayerSpawner>();
        playerSpawner.playerPrefab = playerPrefab;
        
        var coinSpawner = gameObject.AddComponent<CoinSpawner>();
        coinSpawner.coinPrefab = coinPrefab;
        
        var goombaSpawner = gameObject.AddComponent<GoombaSpawner>();
        goombaSpawner.goombaPrefab = goombaPrefab;
        
        // chạy scene có index là 1 trong build settings
        var scene = SceneRef.FromIndex(1);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid)
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Single);
        
        // khởi động NetworkRunner với các thiết lập
        var startGameArgs = new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "SuperMarioWorld", // tên phiên chơi tùy ý
            Scene = sceneInfo,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        };
        await networkRunner.StartGame(startGameArgs);
    }


    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
        
    }

    // chạy khi một player tham gia vào game trong mạng
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
       
    }

    // chạy khi một player rời khỏi game trong mạng
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
        
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }
}
