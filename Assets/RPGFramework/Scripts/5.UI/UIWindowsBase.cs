
using System;
using UnityEngine;
using UnityEngine.AI;


namespace Framework
{
    /// <summary>
    /// Window result
    /// </summary>
    public enum WindowResult
    {
        None,
        Yes,
        No
    }

    /// <summary>
    /// Base class of UI window
    /// </summary>
    public class UIWindowsBase : MonoBehaviour
    {
        /// <summary>
        /// Window type
        /// </summary>
        public Type type
        {
            get { return GetType(); }
        }

        /// <summary>
        /// Initiate window
        /// </summary>
        public virtual void Init()
        {
            AddEventListener();
        }

        /// <summary>
        /// Show window
        /// </summary>
        public virtual void Show()
        {
            OnUpdateLanguage();
        }

        /// <summary>
        /// Close window
        /// </summary>
        public virtual void Close()
        {
            UIManager.Instance.Close(type);
            RemoveEventListener();
        }

        /// <summary>
        /// Event when clocs is clicked
        /// </summary>
        public virtual void OnCloseClick()
        {
            Close();
        }

        /// <summary>
        /// Event when yes is clicked
        /// </summary>
        public virtual void OnYesClick()
        {
            Close();
        }

        /// <summary>
        /// Register Event listener to window
        /// </summary>
        protected virtual void AddEventListener()
        {
            EventManager.AddEventListener("UpdateLanguage", OnUpdateLanguage);
        }

        /// <summary>
        /// Remove Event Listener
        /// </summary>
        protected virtual void RemoveEventListener()
        {
            EventManager.RemoveEventListener("UpdateLanguage");
        }

        /// <summary>
        /// Event to update localization 
        /// </summary>
        protected virtual void OnUpdateLanguage()
        {

        }
    }
}