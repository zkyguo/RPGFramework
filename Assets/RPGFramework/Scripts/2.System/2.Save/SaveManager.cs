using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Save data
/// </summary>
public class SaveItem
{
    public int SaveID { get; private set; }
    public DateTime LastSaveTime { get; private set; }

    public SaveItem(int saveID, DateTime lastSaveTime)
    {
        SaveID = saveID;
        LastSaveTime = lastSaveTime;
    }

    public void UpdateLastSavingTime(DateTime lastSaveTime)
    {
        LastSaveTime = lastSaveTime;
    }
}

public class SaveManager : BaseManager<SaveManager>
{
 
    /// <summary>
    /// Manager config data
    /// </summary>
    private class SaveManagerData
    {
        /// <summary>
        /// Manager increase ID when a new save object has been made to record save object number. It starts by 0
        /// </summary>
        public int currentID = 0;

        /// <summary>
        /// All save data in game
        /// </summary>
        public List<SaveItem> saveItemList = new List<SaveItem>();


    }

    private static SaveManagerData saveManagerData = new SaveManagerData();

    //Save data file name
    private const string savingDirName = "SaveData";
    //Save setting file name
    private const string settingDirName = "Setting";

    //Saving file path
    private static readonly string saveDirPath;
    private static readonly string settingDirPath;

    //Save object cache
    //<saveID,<dir name, save object>>
    private static Dictionary<int, Dictionary<string, object>> saveCacheDic = new Dictionary<int, Dictionary<string, object>>();


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

    #region Save Data
    
    /// <summary>
    /// Get save item by saveID
    /// </summary>
    /// <param name="saveID"></param>
    /// <returns></returns>
    public static SaveItem GetSaveItem(int saveID)
    {
        for (int i = 0; i < saveManagerData.saveItemList.Count; i++)
        {
            SaveItem item = saveManagerData.saveItemList[i];
            if (item.SaveID == saveID)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// Create a new save item
    /// </summary>
    /// <returns></returns>
    public static SaveItem CreateSaveItem()
    {
        SaveItem saveItem = new SaveItem(saveManagerData.currentID,DateTime.Now);
        saveManagerData.saveItemList.Add(saveItem);
        saveManagerData.currentID += 1;
        //TODO : update saveManagerData to save dir
        return saveItem;
    }
    #endregion

    /// <summary>
    /// Delete a saveItem by save ID
    /// </summary>
    /// <param name="saveID"></param>
    public static void DeleteSaveItem(int saveID)
    {
        DeleteSaveItem(GetSaveItem(saveID));
    }

    /// <summary>
    /// Delete a saveItem 
    /// </summary>
    /// <param name="saveItem"></param>
    public static void DeleteSaveItem(SaveItem saveItem)
    {
        string itemDir = GetSavePath(saveItem.SaveID,false);
        if (itemDir != null)
        {
            Directory.Delete(itemDir, true);
        }
        saveManagerData.saveItemList.Remove(saveItem);
    }

    #region Save Object(Serialization)

    /// <summary>
    /// Export(Save) saveObject to save dir
    /// </summary>
    /// <param name="saveObject"></param>
    /// <param name="saveFileName"></param>
    /// <param name="saveID"></param>
    public static void SaveObject(object saveObject, string saveFileName, int saveID = 0)
    {
        //find save dir path
        string dirPath = GetSavePath(saveID);
        //data file in save dir
        string savePath = dirPath + "/" + saveFileName;
        SaveFile(saveObject, savePath);
        //Update saving time
        GetSaveItem(saveID).UpdateLastSavingTime(DateTime.Now);
        // TODO ?Update SaveManagerData

        //Update Save cache
        SetCache(saveID, saveFileName, saveObject);
    }

    /// <summary>
    /// Export(Save) saveItem to save dir 
    /// </summary>
    /// <param name="saveObject"></param>
    /// <param name="saveFileName"></param>
    /// <param name="saveItem"></param>
    public static void SaveObject(object saveObject, string saveFileName, SaveItem saveItem)
    {
        //find save dir path
        string dirPath = GetSavePath(saveItem.SaveID, false);
        //data file in save dir
        string savePath = dirPath + "/" + saveFileName;
        SaveFile(saveObject, savePath);
        //Update saving time
        saveItem.UpdateLastSavingTime(DateTime.Now);
        // TODO ?Update SaveManagerData

        //Update Save cache
        SetCache(saveItem.SaveID, saveFileName, saveObject);
    }

    /// <summary>
    /// Save object to dir which accords saveID
    /// </summary>
    /// <param name="saveObject"></param>
    /// <param name="saveFileName"></param>
    /// <param name="saveID"></param>
    public static void SaveObject(object saveObject, int saveID = 0)
    {
        SaveObject(saveObject, saveObject.GetType().Name, saveID);
    }

    /// <summary>
    /// Save object to dir which accords saveID
    /// </summary>
    /// <param name="saveObject"></param>
    /// <param name="saveFileName"></param>
    /// <param name="saveID"></param>
    public static void SaveObject(object saveObject, SaveItem saveItem)
    {
        SaveObject(saveObject, saveObject.GetType().Name, saveItem);
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
        //Check if object exists in cache
        T cache = GetCache<T>(saveID, saveFileName);

        if(cache == null)
        {
            //save dir path
            string dirPath = GetSavePath(saveID);
            //data file in save dir
            string savePath = dirPath + "/" + saveFileName;
            cache =  LoadFile<T>(savePath);
            SetCache(saveID, saveFileName, cache);
        }
        
        return cache;
    }

    /// <summary>
    /// Load object from save data file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="saveFileName"></param>
    /// <param name="saveItem"></param>
    /// <returns></returns>
    public static T LoadObject<T>(string saveFileName, SaveItem saveItem) where T : class
    {
        return LoadObject<T>(saveFileName, saveItem.SaveID);       
    }

    /// <summary>
    /// Load object by save ID
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="saveFileName"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static T LoadObject<T>(int saveID = 0) where T : class
    {
        return LoadObject<T>(typeof(T).Name, saveID);
    }

    /// <summary>
    /// Load object by save Item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="saveFileName"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static T LoadObject<T>(SaveItem saveItem) where T : class
    {
        return LoadObject<T>(typeof(T).Name, saveItem.SaveID);
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
    /// Serialize Save object to binary
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
    /// Deserialize a save object 
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

    #region Save Cache

    /// <summary>
    /// Set save cache
    /// </summary>
    /// <param name="saveID"></param>
    /// <param name="fileName"></param>
    /// <param name="saveData"></param>
    private static void SetCache(int saveID, string fileName, object saveData)
    {
        if(saveCacheDic.ContainsKey(saveID))
        {
            //Check if saveID has an file
            if (saveCacheDic[saveID].ContainsKey(fileName))
            {
                saveCacheDic[saveID][fileName] = saveData;
            }
            else
            {
                saveCacheDic[saveID].Add(fileName, saveData);
            }
           
        }
        else
        {
            //Add to cache
            saveCacheDic.Add(saveID, new Dictionary<string, object>()
                {
                    {fileName, saveData}
                });
        }
    }

    /// <summary>
    /// Get save cache by ID and fileName
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="saveID"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private static T GetCache<T>(int saveID, string fileName) where T : class
    {
        if(saveCacheDic.ContainsKey(saveID))
        {
            if (saveCacheDic[saveID].ContainsKey(fileName))
            {
                return saveCacheDic[saveID][fileName] as T;
            }
            
        }
        return null;
    }
    #endregion


}
