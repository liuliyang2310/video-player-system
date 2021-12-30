using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

public class NetworkManager_Pong : NetworkManager
{

    [SerializeField] Transform leftSpawn;
    [SerializeField] Transform rightSpawn;
    GameObject ball;
    /// <summary>
    /// Called on the server when a client adds a new player with ClientScene.AddPlayer.
    /// <para>The default implementation for this function creates a new player object from the playerPrefab.</para>
    /// </summary>
    /// <param name="conn">Connection from client.</param>
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
       // base .OnServerAddPlayer (conn );
        Transform start = numPlayers == 0 ? leftSpawn : rightSpawn;

        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
        if (numPlayers == 2)
        {
            ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
            NetworkServer.Spawn(ball);
        }

    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (ball != null) NetworkServer.Destroy(ball);
         base.OnServerDisconnect(conn);
    }
}