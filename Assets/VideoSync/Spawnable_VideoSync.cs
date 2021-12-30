using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.UI;

public class Spawnable_VideoSync : NetworkBehaviour
{
    private VideoController controller;

    public Text seekTimeTxt;

    private bool isPlaying;

    [Header("SyncVars")]
    [SyncVar(hook = nameof(RefreshSeekTime))]
    public float _seekTime;

    private void Awake()
    {
        Debug.Log("Awake");
        controller = GameObject.FindObjectOfType<VideoController>();
    }
    private void Start()
    {
        Debug.Log("Start:_" + isServer);
    }

    public override void OnStartServer()
    {
        // base.OnStartServer();
        Debug.Log("StartServer");
    }

    public override void OnStartClient()
    {
        Debug.Log("OnStartClient");


        if (isServer)
        {
            controller.StartPlay();
            Debug.Log(controller);
        }
        else
        {
            // controller.StartPlay();
        }

    }

    [ServerCallback]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseOrContinue(controller.GetCurrentTimeMs());
        }
    }


    [ClientRpc]
    private void PauseOrContinue(float ms)
    {
        controller.PauseOrContinue();
        StartCoroutine(DelayMatch(ms));
    }
    IEnumerator DelayMatch(float timeMs)
    {
        yield return null;
        yield return null;
        controller.SeekTimeTo(timeMs);
        Debug.Log("Mach_" + timeMs + "Resault_" + controller.GetCurrentTimeMs());
    }

    [ClientRpc]
    public void StartPlay()
    {

        if (isServer)
        {
            controller.Play();
        }
        else
        {
            controller.Play();
        }
    }
    [ClientRpc]
    public void Pause()
    {

        if (isServer)
        {
            controller.Pause();
            UpdateSeekTime();

        }
        else
        {
            controller.Pause();
        }

        Debug.Log("Pause_" + controller.GetCurrentTimeMs());

    }

    [ServerCallback]
    public void UpdateSeekTime()
    {
        if (controller != null)
        {
            _seekTime = controller.GetCurrentTimeMs();
            Debug.Log("updateSeek" + _seekTime);
        }
    }
    private void RefreshSeekTime(float _, float value)
    {
        string t = (value / 1000).ToString("F3");
        seekTimeTxt.text = t;
        StartCoroutine(WaitRefresh(value));
    }

    IEnumerator WaitRefresh(float value)
    {
        controller.Stop();
        yield return new WaitForSeconds(0.3f);
        controller.SeekTimeTo(value);
        float time = controller.GetCurrentTimeMs() / 1000;
        Debug.Log("Refresh" + time.ToString("F3"));
        yield return null;
        controller.Play();

    }
    public override void OnStopClient()
    {


        controller.Rewind();
        Debug.Log("StopClient");
        base.OnStopClient();
        // controller
    }
    private void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}
