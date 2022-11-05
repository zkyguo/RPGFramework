using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


/// <summary>
/// Basic Game setting : 
/// Default screen resolution, music, gamplay graphic, UI
/// </summary>
[CreateAssetMenu(fileName = "GameSetting", menuName = "RPGFramework/Config/GameSetting ")]
public class GameSetting : ConfigBase
{
    [LabelText("Cache Dic Setting")]
    [DictionaryDrawerSettings(KeyLabel = "Type", ValueLabel = "Any")]
    public Dictionary<Type, bool> CacheDic = new Dictionary<Type, bool>();

#if UNITY_EDITOR


    [Button(Name = "Initialize GameSetting", ButtonHeight = 50)]
    [GUIColor(0,1,0)]
    /// <summary>
    /// Execute while Editor is loading
    /// </summary>
    public void InitPoolAttribute()
    {

        PoolAttributeOnEditor();
    }

    /// <summary>
    /// Put class which can put in Cache(by attribute) to cache dic 
    /// </summary>
    private void PoolAttributeOnEditor()
    {
        CacheDic?.Clear();
        //Get all assembly
        System.Reflection.Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        foreach(var Assembly in Assemblies)
        {
            //Get all types in assemblu
            Type[] types = Assembly.GetTypes(); 
            foreach(Type type in types)
            {
                // Check if type has Pool as attribute which can be put in cache pool
                PoolAttribute pool = type.GetCustomAttribute<PoolAttribute>();
                if(pool != null)
                {
                    CacheDic.Add(type, true);
                }
            }
        }
    }
#endif
}
