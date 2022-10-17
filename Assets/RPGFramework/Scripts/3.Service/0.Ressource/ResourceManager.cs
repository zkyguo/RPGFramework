using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ResourceManager : BaseManager<ResourceManager>
{
    private Dictionary<Type, bool> CacheDic;

    public override void Init()
    {
        base.Init(); 
        CacheDic = new Dictionary<Type, bool>();
        CacheDic.Add(typeof(Bullet), true);
        
    }

    /// <summary>
    /// Get prefab by path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
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
    /// Load assets in unity : Audioclip, png, sprite
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    public T LoadAsset<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }

    /// <summary>
    ///  Load asy gameObject
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="callBack"></param>
    /// <param name="parent"></param>
    public void LoadGameObjectAsync<T>(string path, Action<T> callBack = null, Transform parent = null) where T : UnityEngine.Object
    {
        
        if (CheckCacheDic(typeof(T)))
        {
            GameObject obj = PoolManager.Instance.CheckCacheAndLoadGameObject(path, parent); 
            if(obj != null)
            {
                callBack?.Invoke(obj.GetComponent<T>());
                return;
            }
        }
        StartCoroutine(ExecuteLoadGameObjectAsync<T>(path, callBack, parent));
    }

    /// <summary>
    /// Execution of GameObject asy loading
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="callBack"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    IEnumerator ExecuteLoadGameObjectAsync<T>(string path, Action<T> callBack = null, Transform parent = null) where T : UnityEngine.Object
    {
        ResourceRequest request = Resources.LoadAsync<GameObject>(path);
        yield return request;
        GameObject obj = InstantiatePrefab(request.asset as GameObject, parent);
        callBack?.Invoke(obj.GetComponent<T>());
    }

    /// <summary>
/// Load asy assets in unity
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="path"></param>
/// <param name="callBack"></param>
    public void LoadAssetAsync<T>(string path, Action<T> callBack) where T : UnityEngine.Object
    {
        StartCoroutine(ExecuteLoadAsset<T>(path, callBack));
    }

    /// <summary>
    /// Execution of asset asy loading
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <param name="callBack"></param>
    /// <returns></returns>
    IEnumerator ExecuteLoadAsset<T>(string path, Action<T> callBack) where T : UnityEngine.Object
    {
        ResourceRequest request = Resources.LoadAsync<T>(path);
        yield return request;
        callBack?.Invoke(request.asset as T);
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
