using Fusion;
using UnityEngine;

public class CoinSpawner : SimulationBehaviour, ISceneLoadDone
{
    public NetworkPrefabRef coinPrefab;
    
    public void SceneLoadDone(in SceneLoadDoneArgs sceneInfo)
    {
        var spawnPosition = new Vector3[]
        {
            new Vector3(Random.Range(3f, 10f), Random.Range(2.5f, 5f), 0f),
            new Vector3(Random.Range(3f, 10f), Random.Range(2.5f, 5f), 0f),
            new Vector3(Random.Range(3f, 10f), Random.Range(2.5f, 5f), 0f),
            new Vector3(Random.Range(3f, 10f), Random.Range(2.5f, 5f), 0f),
            new Vector3(Random.Range(3f, 10f), Random.Range(2.5f, 5f), 0f),
        };
        if (Runner.IsSharedModeMasterClient)
        {
            foreach (var position in spawnPosition)
            {
                Runner.Spawn(
                    coinPrefab,
                    position,
                    Quaternion.identity
                );
            }
        }
    }
}
