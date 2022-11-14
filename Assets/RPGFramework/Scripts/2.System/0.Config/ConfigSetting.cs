using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Framework
{
    public enum ConfigType
    {
        Equippement, Item, Weapon
    }

    /// <summary>
    /// Unique Config setting of game
    /// include all config setting
    /// </summary>
    [CreateAssetMenu(fileName = "ConfigSetting", menuName = "RPGFramework/ConfigSetting")]
    public class ConfigSetting : ConfigBase
    {
        /// <summary>
        /// Container of all config
        /// < Type, <id, Config>>
        /// </summary>
        [DictionaryDrawerSettings(KeyLabel = "Config Type", ValueLabel = "List")]
        public Dictionary<ConfigType, Dictionary<int, ConfigBase>> ConfigDic = new Dictionary<ConfigType, Dictionary<int, ConfigBase>>();

        /// <summary>
        /// Get config 
        /// </summary>
        /// <typeparam name="T">Type of config</typeparam>
        /// <param name="configType">Name of Config</param>
        /// <param name="id"> id of Config</param>
        /// <returns></returns>
        public T GetConfig<T>(ConfigType configType, int id) where T : ConfigBase
        {
            //Check if type exists
            if (!ConfigDic.ContainsKey(configType))
            {
                throw new System.Exception($"Config {configType} : not found");

            }
            //Check if id exists
            if (!ConfigDic[configType].ContainsKey(id))
            {
                throw new System.Exception($"Config {configType} doesnt contain : {id}");
            }

            return ConfigDic[configType][id] as T;


        }
    }
}