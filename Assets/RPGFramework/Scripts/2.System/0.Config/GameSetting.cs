using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
namespace Framework
{

    /// <summary>
    /// Basic Game setting, handle logics for unity editor : 
    /// Default screen resolution, music, gamplay graphic, UI
    /// </summary>
    [CreateAssetMenu(fileName = "GameSetting", menuName = "RPGFramework/Config/GameSetting ")]
    public class GameSetting : ConfigBase
    {
        [LabelText("Cache Dic Setting")]
        [DictionaryDrawerSettings(KeyLabel = "Type", ValueLabel = "Any")]
        public Dictionary<Type, bool> CacheDic = new Dictionary<Type, bool>();

        [LabelText("UI Cache Dic Setting")]
        [DictionaryDrawerSettings(KeyLabel = "Type", ValueLabel = "UI window Data")]
        public Dictionary<Type, UIElement> UIElementCacheDic = new Dictionary<Type, UIElement>();

#if UNITY_EDITOR


        [Button(Name = "Initialize GameSetting", ButtonHeight = 50)]
        [GUIColor(0, 1, 0)]
        /// <summary>
        /// Execute while Editor is loading
        /// </summary>
        public void InitPoolAttribute()
        {

            PoolAttributeOnEditor();
            UIElementAttributeOnEditor();
        }

        /// <summary>
        /// Put class which can put in Cache(by attribute) to cache dic 
        /// </summary>
        private void PoolAttributeOnEditor()
        {
            CacheDic?.Clear();
            //Get all assembly
            System.Reflection.Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var Assembly in Assemblies)
            {
                //Get all types in assemblu
                Type[] types = Assembly.GetTypes();
                foreach (Type type in types)
                {
                    // Check if type has Pool as attribute which can be put in cache pool
                    PoolAttribute pool = type.GetCustomAttribute<PoolAttribute>();
                    if (pool != null)
                    {
                        CacheDic.Add(type, true);
                    }
                }
            }
        }


        /// <summary>
        /// Load all class with UIElement attribute to Editor 
        /// </summary>
        private void UIElementAttributeOnEditor()
        {
            UIElementCacheDic.Clear();
            //Get all assembly
            System.Reflection.Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Type baseType = typeof(UIWindowsBase);
            foreach (var Assembly in Assemblies)
            {
                //Get all types in assemblu
                Type[] types = Assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (baseType.IsAssignableFrom(type) && !type.IsAbstract)
                    {
                        UIElementAttribute attribute = type.GetCustomAttribute<UIElementAttribute>();
                        if (attribute != null)
                        {
                            UIElementCacheDic.Add(type, new UIElement() { isCache = attribute.isCache, prefab = Resources.Load<GameObject>(attribute.resourcePath), layerNum = attribute.LayerNum });
                        }
                    }
                }
            }
        }
#endif
    }
}