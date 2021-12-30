using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using System.IO;
using UnityEngine.UI;
using System;

public class VideoController : MonoBehaviour
{

    public bool isLoop;
    [SerializeField] public MediaPlayer mediaPlayer;
    public string videoFolderName;
    List<string> videoPathsList;
    //  private string videosFolderPath;
    void Start()
    {
        string appPath = System.IO.Directory.GetParent(Application.dataPath).ToString();
        string videosFolderPath = appPath + "/" + videoFolderName;
        videoPathsList = GetVideoPaths(videosFolderPath);
        OpenVideoFileFromVideoList(0);
    }



    public void StartPlay()
    {
        // if (mediaPlayer.Control.IsPlaying()) return;
        // OpenVideoFileFromVideoList(0);
        mediaPlayer.Play();

    }

    public void StartPlay(float seekValue)
    {
        //  OpenVideoFileFromVideoList(0);

        mediaPlayer.Control.Seek(seekValue);
        mediaPlayer.Play();

    }
    /// 从视频列表中选择播放
    /// </summary>
    /// <param name="videoIndex"></param>
    private void OpenVideoFileFromVideoList(int videoIndex)
    {
        if (videoPathsList.Count > videoIndex)
        {
            mediaPlayer.m_VideoPath = videoPathsList[videoIndex];
            if (string.IsNullOrEmpty(mediaPlayer.m_VideoPath) == false)
            {
                mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, mediaPlayer.m_VideoPath, false);
            }
        }

    }
    /// <summary>
    /// 从视频列表中选择播放
    /// </summary>
    /// <param name="videoIndex"></param>
    private void OpenVideoFileFromVideoListNoLoop(int videoIndex)
    {
        if (mediaPlayer.Control.IsLooping())
            mediaPlayer.Control.SetLooping(false);
        if (videoPathsList.Count > videoIndex)
        {
            mediaPlayer.m_VideoPath = videoPathsList[videoIndex];
            if (string.IsNullOrEmpty(mediaPlayer.m_VideoPath) == false)
            {
                mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, mediaPlayer.m_VideoPath, false);

            }
        }
    }



    public static List<string> GetVideoPaths(string viedeoFolderPath)
    {
        List<string> cacheList = new List<string>();
        foreach (string f in Directory.GetFiles(viedeoFolderPath, "*", SearchOption.AllDirectories))
        {
            string extName = Path.GetExtension(f).ToLower();
            if (extName == ".mp4" || extName == ".mov" || extName == "avi")
            {
                cacheList.Add(f);

            }
        }
        return cacheList;
    }


    private void StartPlayVideo()
    {
        mediaPlayer.Rewind(false);
        mediaPlayer.Play();
    }

    public void Rewind()
    {
        mediaPlayer.Rewind(true);
    }
    public void Stop()
    {
        mediaPlayer.Stop();
    }
    public void CloseVideo()
    {
        mediaPlayer.CloseVideo();
    }
    public void PauseOrContinue()
    {
        if (mediaPlayer.Control.IsPlaying() == true)
        {
            mediaPlayer.Pause();
        }
        else if (mediaPlayer.Control.IsPaused() == true)
        {
            mediaPlayer.Play();
        }
    }
    public void Pause()//暂停播放
    {
        if (mediaPlayer.Control.IsPlaying() == true)
            mediaPlayer.Pause();
    }

    public void Play()
    {
        // if (mediaPlayer.Control.IsPaused() == true)
        mediaPlayer.Play();
    }

    public float GetCurrentTimeMs()
    {
        return mediaPlayer.Control.GetCurrentTimeMs();
    }

    public void SeekTimeTo(float seekValue)
    {
        mediaPlayer.Control.Seek(seekValue);
    }
    private float playerVolume = 1;
    private void SetVolume(float value)
    {
        playerVolume = Mathf.Clamp(playerVolume + value, 0, 1);
        mediaPlayer.Control.SetVolume(playerVolume);
        Debug.Log("播放器音量：" + playerVolume);
    }
    public void Volume(float value)
    {
        //  MMediaPlayer.Control.SetVolume(0.2f);
        float currentVol = mediaPlayer.Control.GetVolume();
        currentVol += value;
        if (currentVol < 0) currentVol = 0;
        if (currentVol > 1) currentVol = 1;
        mediaPlayer.Control.SetVolume(currentVol);
        Debug.Log(currentVol);
    }
}
