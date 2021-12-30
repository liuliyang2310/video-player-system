using System.Collections.Generic;
using UnityEngine;
using Mirror;
using RenderHeads.Media.AVProVideo;
using System;
using UnityEngine.UI;
using System.Collections;
/*
Documentation: https://mirror-networking.gitbook.io/docs/guides/networkbehaviour
API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

// NOTE: Do not put objects in DontDestroyOnLoad (DDOL) in Awake.  You can do that in Start instead.

public class Player_VideoSync : NetworkBehaviour
{

    private MediaPlayer player;
    public Text seekTimeTxt;


    [Header("SyncVars")]
    [SyncVar(hook = nameof(RefreshSeekTime))]
    public float seekTime;
    #region Start & Stop Callbacks

    /// <summary>
    /// This is invoked for NetworkBehaviour objects when they become active on the server.
    /// <para>This could be triggered by NetworkServer.Listen() for objects in the scene, or by NetworkServer.Spawn() for objects that are dynamically created.</para>
    /// <para>This will be called for objects on a "host" as well as for object on a dedicated server.</para>
    /// </summary>
    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("OnStartServer：" + this.name);
    }

    /// <summary>
    /// Invoked on the server when the object is unspawned
    /// <para>Useful for saving object data in persistent storage</para>
    /// </summary>
    public override void OnStopServer()
    {
        base.OnStopServer();
    }

    /// <summary>
    /// Called on every NetworkBehaviour when it is activated on a client.
    /// <para>Objects on the host have this function called, as there is a local client on the host. The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on the client.</para>
    /// </summary>
    public override void OnStartClient()
    {
        base.OnStartClient();

    }

    /// <summary>
    /// This is invoked on clients when the server has caused this object to be destroyed.
    /// <para>This can be used as a hook to invoke effects or do client specific cleanup.</para>
    /// </summary>
    public override void OnStopClient()
    {
        base.OnStopClient();
        Debug.Log("StopClient");

    }

    /// <summary>
    /// Called when the local player object has been set up.
    /// <para>This happens after OnStartClient(), as it is triggered by an ownership message from the server. This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.</para>
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        player = GameObject.FindObjectOfType<MediaPlayer>();
        Debug.Log("StartLocalPlayer_" + player);
    }

    [ServerCallback]
    private void Update()
    {
        if (player == null) return;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogFormat("{0}:{2}:{1:000}", DateTime.Now.Minute, DateTime.Now.Millisecond, DateTime.Now.Second);
            if (player.Control.IsPlaying())
            {
                Pause();
            }
            else
            {
                StartPlay();
            }
            //   StartCoroutine(DelaySeekTime());
        }
    }
    IEnumerator DelaySeekTime()
    {
        yield return null;
        UpdateSeekTime();
    }
    [ClientRpc]
    public void StartPlay()
    {
        if (!isLocalPlayer) return;
        Debug.Log("ClientOnly_" + isClientOnly);
        Debug.LogFormat("S_{0}:{2}:{1:000}", DateTime.Now.Minute, DateTime.Now.Millisecond, DateTime.Now.Second);

        //  Debug.Log("StartPlay_ClientRct");
        //    _player.Control.SeekFast(_seekTime);
        player.Control.Play();
        Debug.Log(player.Control.GetCurrentTimeMs());

    }
    [ClientRpc]
    public void Pause()
    {
        if (!isLocalPlayer) return;
        Debug.LogFormat("P_{0}:{2}:{1:000}", DateTime.Now.Minute, DateTime.Now.Millisecond, DateTime.Now.Second);

        player.Control.Pause();
        // _player.Control.SeekFast(_seekTime);
        //  Debug.Log(_player.Control.GetCurrentTimeMs());

    }

    [ServerCallback]
    private void UpdateSeekTime()
    {
        if (player.Control != null)
        {
            seekTime = player.Control.GetCurrentTimeMs();
            //Debug.Log(_seekTime);
        }

    }
    private void RefreshSeekTime(float _, float value)
    {
        string t = (value / 1000).ToString("F3");
        seekTimeTxt.text = t;
        player.Control.Seek(value);
        Debug.Log("NewValue_" + t);
        float time = player.Control.GetCurrentTimeMs() / 1000;
        Debug.Log("Refresh" + time.ToString("F3"));
    }

    #endregion
}
