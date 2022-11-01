using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using UnityEngine;

public class SaveManager : BaseManager<SaveManager>
{
    
    //Save data file name
    private const string savingDirName = "SaveData";
    //Save setting file name
    private const string settingDirName = "Setting";

    //Saving file path
    private static readonly string saveDirPath;
    private static readonly string settingDirPath;

    static SaveManager()
    {
        saveDirPath = Application.persistentDataPath + "/" + savingDirName;
        settingDirPath = Application.persistentDataPath + "/" + settingDirName;

        if(!Directory.Exists(saveDirPath))
        { 
            Directory.CreateDirectory(saveDirPath);
        }
        if(!Directory.Exists(settingDirPath))
        {
            Directory.CreateDirectory(settingDirPath);
        }
    }

    #region Objects

    /// <summary>
    /// Save object in data save file
    /// </summary>
    /// <param name="saveObject"></param>
    /// <param name="saveFileName"></param>
    /// <param name="saveID"></param>
    public static void SaveObject(object saveObject, string saveFileName, int saveID = 0)
    {
        //save dir path
        string dirPath = GetSavePath(saveID);
        //data file in save dir
        string savePath = dirPath + "/" + saveFileName;
        SaveFile(saveObject, savePath);
    }

    /// <summary>
    /// Load object from save data file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="saveFileName"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static T LoadObject<T>(string saveFileName, int saveID = 0) where T : class
    {
        //save dir path
        string dirPath = GetSavePath(saveID);
        //data file in save dir
        string savePath = dirPath + "/" + saveFileName;
        
        return LoadFile<T>(savePath);

    }

    #endregion

    #region Tool methods

    private static BinaryFormatter binaryFormatter = new BinaryFormatter();

    /// <summary>
    /// Get save file path by saveID
    /// </summary>
    /// <param name="saveID"></param>
    /// <param name="createDir"></param>
    /// <returns></returns>
    private static string GetSavePath(int saveID, bool createDir =true)
    {
        //Check if save file exists 
        string saveDir = saveDirPath + "/" + saveID; 

        if(!Directory.Exists(saveDir))
        {
            Directory.CreateDirectory(saveDir);
        }
        else
        {
            return null;
        }

        return saveDir;
    }

    /// <summary>
    /// Save file
    /// </summary>
    /// <param name="saveObject">Object to save</param>
    /// <param name="path">save path</param>
    private static void SaveFile(object saveObject, string path)
    {
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
        binaryFormatter.Serialize(fs, saveObject);   
        fs.Dispose();
    }
    
    /// <summary>
    /// Load file
    /// </summary>
    /// <typeparam name="T">Type to convert after loading</typeparam>
    /// <param name="path"> Path of saving file</param>
    /// <returns></returns>
    private static T LoadFile<T>(string path) where T : class 
    {
        if(!File.Exists(path))
        {
            return null;
        }

        FileStream file = new FileStream(path, FileMode.Open);
        T obj = (T)binaryFormatter.Deserialize(file);
        file.Dispose();
        return obj;
    }  
    #endregion


}
