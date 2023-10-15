using System;
using UnityEngine;

namespace Devdog.General.Localization
{
    [Serializable]
    public class LocalizedString
    {
        [SerializeField]
        private string _key = LocalizationManager.NoKeyConstant;

        public LocalizedString()
        {
        }

        public LocalizedString(string key)
        {
            _key = key;
        }

        /// <summary>
        ///     Gets the message in the currently selected language.
        /// </summary>
        [IgnoreCustomSerialization]
        public string message
        {
            get => LocalizationManager.GetString(_key);
            set
            {
                if (LocalizationManager.currentDatabase != null)
                    LocalizationManager.currentDatabase.SetString(_key, value);
            }
        }

        public override string ToString()
        {
            return message;
        }
    }
}