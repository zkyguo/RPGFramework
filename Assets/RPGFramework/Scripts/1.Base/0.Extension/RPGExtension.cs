using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

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
}
