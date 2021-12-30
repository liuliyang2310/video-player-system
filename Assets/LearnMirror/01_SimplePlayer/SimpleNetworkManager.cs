using Mirror;
using UnityEngine;

public class SimpleNetworkManager : NetworkManager
{

    int clientIndex;
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        GameObject go = Instantiate(playerPrefab);
        SimpleDataSync simpleData = go.GetComponent<SimpleDataSync>();
        simpleData.playerColor = Random.ColorHSV(0, 1, 0.9f, 0.9f, 1, 1);
        simpleData.playerNumber = clientIndex;
        // increment the index after setting on player, so first player starts at 0
        clientIndex++;

        NetworkServer.AddPlayerForConnection(conn, go);
    }
}
