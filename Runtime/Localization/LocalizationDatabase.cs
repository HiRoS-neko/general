using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Devdog.General.Localization
{
    [Serializable]
    [CreateAssetMenu(menuName = "Devdog/Localization Database")]
    public class LocalizationDatabase : BetterScriptableObject
    {
        [SerializeField]
        private string _lang = "en-US";

        [SerializeField]
        private Dictionary<string, Object> _localizedObjects = new();

        [SerializeField]
        private Dictionary<string, string> _localizedStrings = new()
        {
            { LocalizationManager.NoKeyConstant, string.Empty }
        };

        public string lang
        {
            get => _lang;
            set => _lang = value;
        }


        public bool ContainsString(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;

            return _localizedStrings.ContainsKey(key);
        }

        public string GetString(string key, string notFound = "")
        {
            if (string.IsNullOrEmpty(key)) return notFound;

            string result;
            var got = _localizedStrings.TryGetValue(key, out result);
            if (got) return result;

            return notFound;
        }

        public void SetString(string key, string msg)
        {
            if (string.IsNullOrEmpty(key)) return;

            if (string.IsNullOrEmpty(msg))
            {
                DeleteString(key);
                return;
            }

            _localizedStrings[key] = msg;

#if UNITY_EDITOR
//            if(UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode || UnityEditor.EditorApplication.isPlaying == false)
//            {
            EditorUtility.SetDirty(this);
//            }
#endif
        }

        public void DeleteString(string key)
        {
            if (string.IsNullOrEmpty(key)) return;

            _localizedStrings.Remove(key);
        }

        public bool ContainsObject(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;

            return _localizedObjects.ContainsKey(key);
        }

        public T GetObject<T>(string key, T notFound = null) where T : Object
        {
            if (string.IsNullOrEmpty(key)) return notFound;

            Object result;
            var got = _localizedObjects.TryGetValue(key, out result);
            if (got) return (T)result;

            return notFound;
        }

        public void SetObject<T>(string key, T obj) where T : Object
        {
            if (string.IsNullOrEmpty(key)) return;

            if (obj == null)
            {
                DeleteObject(key);
                return;
            }

            _localizedObjects[key] = obj;

#if UNITY_EDITOR
//            if(UnityEditor.EditorApplication.isPlaying == false)
//            {
            EditorUtility.SetDirty(this);
//            }
#endif
        }

        public void DeleteObject(string key)
        {
            if (string.IsNullOrEmpty(key)) return;

            _localizedObjects.Remove(key);
        }


#if UNITY_EDITOR

        public Dictionary<string, string> _EditorGetAllStrings()
        {
            return _localizedStrings;
        }

        public Dictionary<string, Object> _EditorGetAllObjects()
        {
            return _localizedObjects;
        }

#endif
    }
}