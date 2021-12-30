using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class AssetsLoader<T>
{
    public static T[] GetJsonObjects(string jsonPath)
    {
        // string jsonPath = AppSetting.Instence.OuterFolderPath + "/" + jsonName;
        StreamReader streamReader = new StreamReader(jsonPath);
        string jsonStr = streamReader.ReadToEnd();
        if (jsonStr.Length <= 0)
        {
            Debug.LogError("找不到配置信息");
            return default;
        }
        else
        {
            return JsonUtility.FromJson<T[]>(jsonStr);
            // return JsonUtility.ToObject<T[]>(jsonStr);
        }
    }
    public static T GetJsonObject(string jsonPath)
    {
        // string jsonPath = AppSetting.Instence.OuterFolderPath + "/" + jsonName;
        StreamReader streamReader = new StreamReader(jsonPath);
        string jsonStr = streamReader.ReadToEnd();
        if (jsonStr.Length <= 0)
        {
            Debug.LogError("找不到配置信息");
            return default;
        }
        else
        {
           return JsonUtility.FromJson<T>(jsonStr);
           // return JsonMapper.ToObject<T>(jsonStr);
        }
    }

    public static List<Texture> LoadStreamingAssetsPicture(string folderName)
    {
        List<Texture> cacheList = new List<Texture>();

        foreach (string f in Directory.GetFiles(Application.streamingAssetsPath + "/" + folderName))
        {
            string extName = Path.GetExtension(f).ToLower();
            if (extName == ".jpg" || extName == ".png")
            {
                Texture2D tex = new Texture2D(10, 10, TextureFormat.RGB24, false);
                tex.name = Path.GetFileName(f);
                tex.LoadImage(File.ReadAllBytes(f));
                cacheList.Add(tex);
            }
        }
        return cacheList;
    }
    public static List<Texture> LoadAssetsPictureFrom(string pictureFolderPath)
    {
        List<Texture> cacheList = new List<Texture>();

        foreach (string f in Directory.GetFiles(pictureFolderPath))
        {
            string extName = Path.GetExtension(f).ToLower();
            if (extName == ".jpg" || extName == ".png")
            {
                Texture2D tex = new Texture2D(10, 10, TextureFormat.RGB24, false);
                tex.name = Path.GetFileName(f);
                tex.LoadImage(File.ReadAllBytes(f));
                cacheList.Add(tex);
            }
        }
        return cacheList;
    }

    public static List<Texture> LoadAllStreamingAssetsPicture()
    {
        List<Texture> cacheList = new List<Texture>();

        foreach (string f in Directory.GetFiles(Application.streamingAssetsPath, "*", SearchOption.AllDirectories))
        {
            string extName = Path.GetExtension(f).ToLower();
            if (extName == ".jpg" || extName == ".png")
            {
                Texture2D tex = new Texture2D(10, 10, TextureFormat.RGB24, false);
                tex.name = Path.GetFileName(f);
                tex.LoadImage(File.ReadAllBytes(f));
                cacheList.Add(tex);
                // Debug.Log(tex.name );
            }
        }
        return cacheList;
    }

    /// <summary>
    /// 获取指定路径下面的所有资源文件  
    /// </summary>
    /// <param name="path"></param>
    public static void GetAllFiles(string path)
    {
        if (Directory.Exists(path))
        {
            DirectoryInfo direction = new DirectoryInfo(path);
            FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);

            Debug.Log("文件数量:" + files.Length);

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                Debug.Log("Name:" + files[i].Name);
                Debug.Log("FullName:" + files[i].FullName);


            }
        }
    }



    /// <summary>
    /// 获取指定路径下面的所有文件夹
    /// </summary>
    /// <param name="path"></param>
    public static void GetAllDirectory(string path)
    {
        DirectoryInfo direction = new DirectoryInfo(path);
        DirectoryInfo[] folders = direction.GetDirectories();
        foreach (var item in folders)
        {
            Debug.Log("Name:" + item.Name);
            Debug.Log("FullName:" + item.FullName);
        }
    }
}
