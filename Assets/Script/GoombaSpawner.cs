using Fusion;
using UnityEngine;

public class GoombaSpawner : SimulationBehaviour, ISceneLoadDone
{
    public NetworkPrefabRef goombaPrefab;
    public Vector3[] spawnPositions;
    
    public void SceneLoadDone(in SceneLoadDoneArgs sceneInfo)
    {
        var positions = GameObject.FindGameObjectsWithTag("GoombaSpawnPoint");
        spawnPositions = new Vector3[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            spawnPositions[i] = positions[i].transform.position;
        }
        
        if (Runner.IsSharedModeMasterClient)
        {
            foreach (var position in spawnPositions)
            {
                Runner.Spawn(
                    goombaPrefab,
                    position,
                    Quaternion.identity
                );
            }
        }
    }
}
