using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Rendering.HybridV2;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// Main extensions about RPGFramework
/// </summary>
public static class RPGExtension
{
    #region Commun use
    /// <summary>
    /// get attribute of object
    /// </summary>
    /// <param name="obj"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetAttribute<T>(this object obj) where T : Attribute
    {
        return obj.GetType().GetCustomAttribute<T>();
    }

    /// <summary>
    /// get attribute of an specified type
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="type"></param>
    /// <typeparam name="T"> class where attribute belong</typeparam>
    /// <returns></returns>
    public static T GetAttribute<T>(this object obj, Type type) where T : Attribute
    {
        return type.GetCustomAttribute<T>();
    }

    /// <summary>
    /// Check if two arrays are equals by comparing all their elements
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool ArrayEquals(this object[] objects, object[] other)
    {
        if(other == null || objects.GetType() != other.GetType())
        {
            return false;
        }

        if(objects.Length == other.Length)
        {
            for(int i = 0; i < objects.Length; i++)
            {
                if (!objects[i].Equals(other[i]))
                {
                    return false;
                }
            }
            return true;
        }

        return false;

    }

    #endregion

    #region Resource Manage

    /// <summary>
    /// put GameObject in pool
    /// </summary>
    /// <param name="obj"></param>
    public static void GameObjectPushPool(this GameObject obj)
    {
        PoolManager.Instance.PushGameobject(obj);
    }

    /// <summary>
    /// put Component in pool
    /// </summary>
    /// <param name="component"></param>
    public static void GameObjectPushPool(this Component component)
    {
        PoolManager.Instance.PushGameobject(component.gameObject);
    }

    /// <summary>
    /// put non-gameobject Object in pool
    /// </summary>
    /// <param name="obj"></param>
    public static void ObjectPushPool(this object obj)
    {
        PoolManager.Instance.PushObject(obj);
    }

    #endregion

    #region Localisation

    /// <summary>
    /// Set localisation text content
    /// </summary>
    /// <param name="text"></param>
    /// <param name="packageName"></param>
    /// <param name="contentKey"></param>
    public static void LocalSet(this Text text, string packageName, string contentKey)
    {
        text.text = LocalizationManager.Instance.GetContent<L_Text>(packageName, contentKey).content;
    }

    /// <summary>
    /// Set localisation audio content
    /// </summary>
    /// <param name="text"></param>
    /// <param name="packageName"></param>
    /// <param name="contentKey"></param>
    public static void LocalSet(this AudioSource audio, string packageName, string contentKey)
    {
        audio.clip = LocalizationManager.Instance.GetContent<L_Audio>(packageName, contentKey).content;
    }

    /// <summary>
    /// Set localisation image content
    /// </summary>
    /// <param name="text"></param>
    /// <param name="packageName"></param>
    /// <param name="contentKey"></param>
    public static void LocalSet(this Image image, string packageName, string contentKey)
    {
        image.sprite = LocalizationManager.Instance.GetContent<L_Image>(packageName, contentKey).content;
    }

    /// <summary>
    /// Set localisation video content
    /// </summary>
    /// <param name="text"></param>
    /// <param name="packageName"></param>
    /// <param name="contentKey"></param>
    public static void LocalSet(this VideoPlayer video, string packageName, string contentKey)
    {
        video.clip = LocalizationManager.Instance.GetContent<L_Video>(packageName, contentKey).content;
    }
    #endregion

    #region MonoExtension

    /// <summary>
    /// Add updateListener to MonoManager
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="action"></param>
    public static void AddUpdateListener(this object obj, Action action)
    {
        MonoManager.Instance.AddUpdateListener(action);
    }

    /// <summary>
    /// Remove updateListener of MonoManager
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="action"></param>
    public static void RemoveUpdateListener(this object obj, Action action)
    {
        MonoManager.Instance.RemoveUpdateListener(action);
    }

    /// <summary>
    /// Add LateUpdate Listener to MonoManager
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="action"></param>
    public static void AddLateUpdateListener(this object obj, Action action)
    {
        MonoManager.Instance.AddLateUpdateListener(action);
    }

    /// <summary>
    /// Remove LateUpdate Listener of MonoManager
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="action"></param>
    public static void RemoveLateUpdateListener(this object obj, Action action)
    {
        MonoManager.Instance.RemoveLateUpdateListener(action);
    }

    /// <summary>
    /// Add FixedUpdate Listener to MonoManager
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="action"></param>
    public static void AddFixedUpdateListener(this object obj, Action action)
    {
        MonoManager.Instance.AddFixedUpdateListener(action);
    }

    /// <summary>
    /// Remove FixedUpdate Listener of MonoManager
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="action"></param>
    public static void RemoveFixedUpdateListener(this object obj, Action action)
    {
        MonoManager.Instance.RemoveFixedUpdateListener(action);
    }

    /// <summary>
    /// Start a routine
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="routine"></param>
    /// <returns></returns>
    public static Coroutine CoroutineStart(this object obj, IEnumerator routine)
    {
        return MonoManager.Instance.StartCoroutine(routine);
    }

    /// <summary>
    /// Stop a routine
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="routine"></param>
    public static void CoroutineStop(this object obj, Coroutine routine)
    {
        MonoManager.Instance.StopCoroutine(routine);
    }

    /// <summary>
    /// Stop all coroutine
    /// </summary>
    /// <param name="obj"></param>
    public static void AllCoroutineStop(this object obj) 
    {
        MonoManager.Instance.StopAllCoroutines();
    }
    #endregion

}
