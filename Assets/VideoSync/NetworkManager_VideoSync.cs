using System;
using UnityEngine;
using Mirror;
using System.Collections.Generic;
using System.Collections;
/*
Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

public class NetworkManager_VideoSync : NetworkManager
{



    public GameObject spawnPrefab;
    //     public override void OnStartClient()
    //     {
    //         base.OnStartClient();
    //     }
    private Spawnable_VideoSync _VideoSync;
    public override void OnServerAddPlayer(NetworkConnection conn)
    {

        // Debug.Log("OnServerAddPlayer:" + conn);
        if (numPlayers == 0)
        {
            GameObject go = Instantiate(spawnPrefab);
            _VideoSync = go.GetComponent<Spawnable_VideoSync>();
            NetworkServer.Spawn(go);
            //   base.OnServerAddPlayer(conn);
        }
        else
        {
            if (_VideoSync != null)
            {
                _VideoSync.UpdateSeekTime();
            }
        }
        base.OnServerAddPlayer(conn);

        Debug.Log("ServerAddPlayer");
    }
}
