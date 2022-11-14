using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEngine.Video;
using UnityEngine.UI;

namespace Framework
{

    /// <summary>
    /// Languages of game
    /// </summary>
    public enum LanguageType
    {
        English = 0, Francais = 1
    }

    #region Localisation Language content
    /// <summary>
    /// Interface of content
    /// </summary>
    public interface ILanguangeContent
    { }

    /// <summary>
    /// Text language content
    /// </summary>
    [Serializable]
    public class L_Text : ILanguangeContent
    {
        public string content;
    }

    [Serializable]
    public class L_Image : ILanguangeContent
    {
        public Sprite content;
    }

    /// <summary>
    /// Audio localisation content
    /// </summary>
    [Serializable]
    public class L_Audio : ILanguangeContent
    {
        public AudioClip content;
    }

    /// <summary>
    /// Audio localisation content
    /// </summary>
    [Serializable]
    public class L_Video : ILanguangeContent
    {
        public VideoClip content;
    }
    #endregion

    /// <summary>
    /// Language model, which define different language possible on the same object
    /// </summary>
    public class LocalizationModel
    {
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine, KeyLabel = "Language", ValueLabel = "Value")]
        public Dictionary<LanguageType, ILanguangeContent> ContentDic = new Dictionary<LanguageType, ILanguangeContent>()
    {
        {LanguageType.English, new L_Text()},
        {LanguageType.Francais, new L_Text()}
    };


    }

    [CreateAssetMenu(fileName = "Localization", menuName = "RPGFramework/LocalizationConfig")]
    public class LocalizationSetting : ConfigBase
    {
        /// <summary>
        /// <PackageName,<contentKey, languageValue>>
        /// </summary>
        [SerializeField]
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine, KeyLabel = "Data package", ValueLabel = "Language Data")]
        private Dictionary<string, Dictionary<string, LocalizationModel>> DataPackage;

        public T GetContent<T>(string packageName, string contentKey, LanguageType languageType) where T : class, ILanguangeContent
        {
            return DataPackage[packageName][contentKey].ContentDic[languageType] as T;
        }
    }
}