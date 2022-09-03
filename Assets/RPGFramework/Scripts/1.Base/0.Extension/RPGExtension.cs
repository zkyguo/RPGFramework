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
}
