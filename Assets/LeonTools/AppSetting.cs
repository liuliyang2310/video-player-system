using UnityEngine;
using System.IO;
using System.Collections;
using System;
public class AppSetting : MonoBehaviour
{

    public static AppSetting Instence;
    // [Header("屏幕分辨率")]
    // public int ScreenWidth;
    // public int ScreenHeight;


    [Header("隐藏鼠标指针")]
    public bool HideCursor = true;
    public SettingInfo _settingInfo;
    public string _outerFolderName = "程序外置资源目录";//程序外置资源目录

    private string outerFolderPath = null;
    private void Awake()
    {
        Instence = this;
        InitialSettingInfo();
        Cursor.visible = !HideCursor;
    }
    private void Start()
    {
        Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow);
    }
    private void InitialSettingInfo()
    {


        string appFolderPath = System.IO.Directory.GetParent(Application.dataPath).ToString();
        string settingFilePath = appFolderPath + "/" + "AppSetting.json";
        _settingInfo = AssetsLoader<SettingInfo>.GetJsonObject(settingFilePath);
    }

    /// <summary>
    /// 外置程序资源路径
    /// </summary>
    /// <value></value>
    public string OuterFolderPath
    {
        get
        {
            if (!string.IsNullOrEmpty(outerFolderPath))
            {

                return outerFolderPath;
            }
            else
            {
                string appFolderPath = System.IO.Directory.GetParent(Application.dataPath).ToString();

                outerFolderPath = appFolderPath + "/" + _outerFolderName;
                return outerFolderPath;
            }
        }
    }
    // [Tooltip("默认文件路径为StreamingAssets目录下")]
    // public string SettingTextName;
    // /// <summary>
    // /// 获取配置的文本信息从StreamingAssets目录
    // /// </summary>
    // /// <returns></returns>
    // public string GetSettingInfo()
    // {
    //     string fileUrl = Application.streamingAssetsPath + "/" + SettingTextName;

    //     var info = File.ReadAllText(fileUrl);

    //     Debug.Log("配置文本内容:" + info);
    //     return info;
    // }

}
