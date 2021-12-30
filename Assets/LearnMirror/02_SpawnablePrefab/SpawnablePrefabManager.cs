using System;
using UnityEngine;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

public class SpawnablePrefabManager : NetworkManager
{

    [Header("Spawns")]
    public GameObject spawnPrefab;
    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("OnStartServer");
        GameObject go = Instantiate(spawnPrefab);
        go.name = "CubeSpawned";
        NetworkServer.Spawn(go);
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("OnStartClient");

    }

}
