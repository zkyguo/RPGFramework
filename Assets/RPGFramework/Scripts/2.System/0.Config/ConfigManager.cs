
using UnityEngine;

public class ConfigManager : BaseManager<ConfigManager>
{
    [SerializeField]
    private ConfigSetting configSetting;

    /// <summary>
    /// Get config 
    /// </summary>
    /// <typeparam name="T">Type of config</typeparam>
    /// <param name="configType">Name of Config</param>
    /// <param name="id"> id of Config</param>
    /// <returns></returns>
    public T GetConfig<T>(ConfigType configType, int id) where T : ConfigBase
    {
        return configSetting.GetConfig<T>(configType, id);
    }

}
