using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Unique Config setting of game
/// include all config setting
/// </summary>
[CreateAssetMenu(fileName ="ConfigSetting", menuName ="RPGFramework/ConfigSetting")]
public class ConfigSetting : ConfigBase
{
    /// <summary>
    /// Container of all config
    /// </summary>
    [DictionaryDrawerSettings(KeyLabel ="Config Type")]
    public Dictionary<string,Dictionary<int,ConfigBase>> CongfigDic = new Dictionary<string,Dictionary<int,ConfigBase>>();  


}
