using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;
using System.IO;
using UnityEngine.UI;
using System;

public class LeonAvProController : MonoBehaviour
{

    public bool isLoop;
    public MediaPlayer MMediaPlayer;
    //bool isPlaying = false;
    public DisplayUGUI displayUGUI;
    public string videoFolderName;

    public static LeonAvProController Instence;
    string videoFolderPath;
    List<string> videoPathList;
    List<string> videoAnpPicPathList;
    private void Awake()
    {
        Instence = this;

    }
    void Start()
    {
  
        string appPath = System.IO.Directory.GetParent(Application.dataPath).ToString();
        videoFolderPath = appPath + "/" + videoFolderName;
        // videoPathList = GetVideoName(videoFolderPath);
     //   videoAnpPicPathList = GetVideoAndPictureName(videoFolderPath);
        OpenVideoOrPicNoDefaultNoLoop(0);
    }

    public void OnVideoEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
        switch (et)
        {

            case MediaPlayerEvent.EventType.ReadyToPlay:
                break;
            case MediaPlayerEvent.EventType.Started:

                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                // isPlaying = false;
                // DispalyUI.SetActive(false);
                break;
        }
    }


    /// <summary>
    /// 播放单个视频(视频文件夹里的第一视频)
    /// </summary>
    public void OpenVieoFileFromAppRootFolder()
    {

        MMediaPlayer.m_VideoPath = GetVideoName(videoFolderPath)[0];
        if (string.IsNullOrEmpty(MMediaPlayer.m_VideoPath) == false)
        {
            MMediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, MMediaPlayer.m_VideoPath, true);
        }


    }
    /// <summary>
    /// 从视频列表中选择播放
    /// </summary>
    /// <param name="videoIndex"></param>
    private void OpenVideoFileFromVideoList(int videoIndex)
    {

        // MMediaPlayer.m_VideoPath = GetVideoName(videoFolderPath)[videoIndex];
        if (videoPathList.Count > videoIndex)
        {
            string fullPath = videoAnpPicPathList[videoIndex];

            MMediaPlayer.m_VideoPath = videoPathList[videoIndex];
            if (string.IsNullOrEmpty(MMediaPlayer.m_VideoPath) == false)
            {
                MMediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, MMediaPlayer.m_VideoPath, true);
            }
        }

    }
    /// <summary>
    /// 从视频列表中选择播放
    /// </summary>
    /// <param name="videoIndex"></param>
    private void OpenVideoFileFromVideoListNoLoop(int videoIndex)
    {
        if (MMediaPlayer.Control.IsLooping())
            MMediaPlayer.Control.SetLooping(false);

        // MMediaPlayer.m_VideoPath = GetVideoName(videoFolderPath)[videoIndex];
        if (videoPathList.Count > videoIndex)
        {
            string fullPath = videoAnpPicPathList[videoIndex];

            MMediaPlayer.m_VideoPath = videoPathList[videoIndex];
            if (string.IsNullOrEmpty(MMediaPlayer.m_VideoPath) == false)
            {
                MMediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, MMediaPlayer.m_VideoPath, true);

            }
        }

    }


    public RawImage Picture;
    private void OpenVideoOrPicNoDefaultNoLoop(int fIndex)
    {
        MMediaPlayer.Stop();
        // MMediaPlayer.CloseVideo()
        string f = videoAnpPicPathList[fIndex];
        string extName = Path.GetExtension(f).ToLower();

        if (extName == ".jpg" || extName == ".png")
        {
            Texture2D tex = new Texture2D(10, 10, TextureFormat.RGB24, false);
            tex.LoadImage(File.ReadAllBytes(f));
            // displayUGUI._defaultTexture = tex;
            Picture.transform.parent.gameObject.SetActive(true);
            Picture.texture = tex;
            float width = 0;
            float height = 1;
            float tw = tex.width;
            float th = tex.height;
            float ratioScreen = (float)Screen.width / Screen.height;
            //图片宽高比例 
            float xyper = (float)tw / th;
            Debug.Log(ratioScreen);
            Debug.Log(Screen.width + "+" + Screen.height);
            if (ratioScreen >= xyper)
            {

                Debug.Log("横屏竖图以屏幕高度为图片的高度");
                height = Screen.height;
                width = height * xyper;
            }
            else
            {
                width = Screen.width;
                height = width / xyper;
            }
            Picture.rectTransform.sizeDelta = new Vector2(width, height);

        }
        else if (extName == ".wmv" || extName == ".mp4" || extName == ".mov" || extName == ".avi")
        {
            MMediaPlayer.m_VideoPath = f;
            if (string.IsNullOrEmpty(MMediaPlayer.m_VideoPath) == false)
            {
                Picture.transform.parent.gameObject.SetActive(false);
                if (MMediaPlayer.m_Loop != isLoop)
                {
                    MMediaPlayer.m_Loop = isLoop;
                }
                MMediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, MMediaPlayer.m_VideoPath, true);
            }
            SetVolume(0);
        }
    }

    private void OpenVideoOrPic(int fIndex)
    {
        MMediaPlayer.Stop();
        MMediaPlayer.CloseVideo();
        string f = videoAnpPicPathList[fIndex];
        string extName = Path.GetExtension(f).ToLower();

        if (extName == ".jpg" || extName == ".png")
        {
            Texture2D tex = new Texture2D(10, 10, TextureFormat.RGB24, false);
            tex.LoadImage(File.ReadAllBytes(f));
            displayUGUI._defaultTexture = tex;

        }
        else if (extName == ".wmv" || extName == ".mp4" || extName == ".mov" || extName == ".avi")
        {
            MMediaPlayer.m_VideoPath = f;
            if (string.IsNullOrEmpty(MMediaPlayer.m_VideoPath) == false)
            {
                MMediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, MMediaPlayer.m_VideoPath, true);
            }
        }
    }
    private Texture OpenPictureFile(string f)
    {
        string extName = Path.GetExtension(f).ToLower();
        if (extName == ".jpg" || extName == ".png")
        {
            Texture2D tex = new Texture2D(10, 10, TextureFormat.RGB24, false);
            tex.LoadImage(File.ReadAllBytes(f));
            return tex;
        }
        return null;
    }
    public void OnOpenVideoFileFromStreamingAsset()
    {

        MMediaPlayer.m_VideoPath = GetVideoName(Application.streamingAssetsPath)[0];
        if (string.IsNullOrEmpty(MMediaPlayer.m_VideoPath) == false)
        {
            MMediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, MMediaPlayer.m_VideoPath, false);
            MMediaPlayer.m_AutoOpen = true;
        }
    }
    public static List<string> GetVideoName(string viedeoFolderPath)
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
        // DispalyUI.SetActive(true);
        //   isPlaying = true;
        MMediaPlayer.Rewind(false);
        MMediaPlayer.Play();
        //Debug.Log("StartPlayVideo");

    }

    private void StopPlayVideo()
    {
        //  isPlaying = false;
        // DispalyUI.SetActive(false);
        MMediaPlayer.Stop();
        // Debug.Log("StopPlayVideo");
    }


    private void PauseOrContinue()
    {
        if (MMediaPlayer.Control.IsPlaying() == true)
        {
            MMediaPlayer.Pause();
        }
        else if (MMediaPlayer.Control.IsPaused() == true)
        {
            MMediaPlayer.Play();
        }
    }
    private void Pause()//暂停播放
    {
        if (MMediaPlayer.Control.IsPlaying() == true)
            MMediaPlayer.Pause();
    }

    private void Continue()
    {
        if (MMediaPlayer.Control.IsPaused() == true)
            MMediaPlayer.Play();
    }


    private float playerVolume = 1;
    private void SetVolume(float value)
    {
        playerVolume = Mathf.Clamp(playerVolume + value, 0, 1);
        MMediaPlayer.Control.SetVolume(playerVolume);
        Debug.Log("播放器音量：" + playerVolume);
    }
    public void Volume(float value)
    {
        //  MMediaPlayer.Control.SetVolume(0.2f);
        float currentVol = MMediaPlayer.Control.GetVolume();
        currentVol += value;
        if (currentVol < 0) currentVol = 0;
        if (currentVol > 1) currentVol = 1;
        MMediaPlayer.Control.SetVolume(currentVol);
        Debug.Log(currentVol);
    }
}
