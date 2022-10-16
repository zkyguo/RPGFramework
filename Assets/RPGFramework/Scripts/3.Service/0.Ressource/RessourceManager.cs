using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class RessourceManager : BaseManager<RessourceManager>
{
    private Dictionary<Type, bool> CacheDic;

    public override void Init()
    {
        base.Init(); 
        CacheDic = new Dictionary<Type, bool>();
    }

    public GameObject GetPrefab(string path)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        if(prefab == null)
        {
            throw new Exception($"Cannont find prefab with path : {path}");
        }

        return prefab;

    }

    /// <summary>
    /// Check if non-gameobject exists in cache dic
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private bool CheckCacheDic(Type type)
    {
        return CacheDic.ContainsKey(type);  
    }

    /// <summary>
    /// Get instance of non-gameobject , if non-gameobject exists in cacheDic, get from cacheDic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Load<T>() where T : class, new()
    {
        if(CheckCacheDic(typeof(T)))
        {
            return PoolManager.Instance.GetObject<T>(); 
        }

        return new T();
    }

    /// <summary>
    /// Get instance of gameobject
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public T Load<T>(String path, Transform parent) where T : Component
    {
        if(CheckCacheDic(typeof(T)))
        {
            return PoolManager.Instance.GetGameobject<T>(GetPrefab(path), parent);
        }

        return InstantiatePrefab(path).GetComponent<T>();
    }

    /// <summary>
    /// Instantiate prefab by prefab path
    /// </summary>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject InstantiatePrefab(string path, Transform parent = null)
    {
        return InstantiatePrefab(GetPrefab(path), parent);
    }

    /// <summary>
    /// Instantiate prefab by prefab gameobject
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject InstantiatePrefab(GameObject prefab, Transform parent = null)
    {
        GameObject obj = GameObject.Instantiate<GameObject>(prefab, parent);
        obj.name = prefab.name;
        return obj;

    }

}
