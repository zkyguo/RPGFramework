using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Framework
{

    /// <summary>
    /// Localization Manager
    /// </summary>
    public class LocalizationManager : BaseManager<LocalizationManager>
    {
        public LocalizationSetting localizationSetting;

        private LanguageType _currentLanguageType;

        [SerializeField]
        [OnValueChanged("UpdateLanguage")]
        public LanguageType CurrentLanguageType
        {

            get => _currentLanguageType;
            set
            {
                _currentLanguageType = value;
                UpdateLanguage();
            }
        }

        private void UpdateLanguage()
        {
#if UNITY_EDITOR
            GameRoot.InitEditor();
#endif
            EventManager.EventTrigger("UpdateLanguage");
        }

        /// <summary>
        /// Get the content of current localizationSetting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="packageName"></param>
        /// <param name="contentKey"></param>
        /// <returns></returns>
        public T GetContent<T>(string packageName, string contentKey) where T : class, ILanguangeContent
        {
            return localizationSetting.GetContent<T>(packageName, contentKey, CurrentLanguageType);
        }
    }

}