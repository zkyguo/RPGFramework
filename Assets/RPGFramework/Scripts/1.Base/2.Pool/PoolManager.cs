using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting;
using UnityEngine;

public class PoolManager : BaseManager<PoolManager>
{
    //Root
    [SerializeField]
    private GameObject PoolRootObj;

    /// <summary>
    /// container of gameobject
    /// </summary>
    public Dictionary<string, GameObjectPoolData> GameObjectPoolDic = new Dictionary<string, GameObjectPoolData>();

    public override void Init()
    {
        base.Init();
        
    }

    /// <summary>
    /// Get component, per exemple :
    /// Bullet has bullet script/component
    /// Enemy has Enemy script/component
    /// </summary>
    /// <param name="prefab"></param>
    /// <typeparam name="T">The component which you need</typeparam>
    /// <returns></returns>
    public T GetGameobject<T>(GameObject prefab) where T : UnityEngine.Object
    {
        GameObject obj = GetGameobject(prefab);
        if (obj != null)
        {
            return obj.GetComponent<T>();
        }

        return null;
    }
    
    /// <summary>
    /// Get component
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public GameObject GetGameobject (GameObject prefab) 
    {
        GameObject obj = null;
        string name = prefab.name;
        
        //Check if gameObject exists in pool
        if (CheckGameobjectCache(prefab))
        {
            obj = GameObjectPoolDic[name].GetObj();
        }
        //if not, instantiate it
        else
        {
            obj = GameObject.Instantiate(prefab);
            obj.name = name;
        }
        return obj;
    }

    /// <summary>
    /// Push gameobject to object Pool (when object finishs its mission)
    /// </summary>
    /// <param name="obj"></param>
    public void PushGameobject(GameObject obj)
    {
        string name = obj.name;
        //check if obj exists in pool
        if (GameObjectPoolDic.ContainsKey(name))
        {
            GameObjectPoolDic[name].PushObj(obj);
        }
        //if not, create it
        else
        {
            GameObjectPoolDic.Add(name, new GameObjectPoolData(obj, PoolRootObj));
        }

    }

    /// <summary>
    /// Check if data exists in pool
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    private bool CheckGameobjectCache(GameObject prefab)
    {
        string name = prefab.name;
        return GameObjectPoolDic.ContainsKey(name) && GameObjectPoolDic[name].PoolQueue.Count > 0;
    }

    public void Clear()
    {
        GameObjectPoolDic.Clear();
    }
}
