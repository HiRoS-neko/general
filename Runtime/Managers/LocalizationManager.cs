using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace Devdog.General.Localization
{
    [ExecuteInEditMode]
    public class LocalizationManager : ManagerBase<LocalizationManager>
    {
        public const string NoKeyConstant = "NO_KEY";

        private static string[] _availableLanguageNames = new string[0];

        [SerializeField]
        [Required]
        private LocalizationDatabase _defaultDatabase;

        [SerializeField]
        private LocalizationDatabase[] _databases = new LocalizationDatabase[0];

        public static LocalizationDatabase defaultDatabase
        {
            get
            {
                if (instance == null) return null;

                return instance._defaultDatabase;
            }
            set
            {
                if (instance == null)
                {
                    DevdogLogger.LogError(
                        "Trying to set default database, but no manager instance was found (add the manager to the _Managers object!");
                    return;
                }

                instance._defaultDatabase = value;
            }
        }

        public static LocalizationDatabase currentDatabase { get; private set; }

        public static string currentLanguage
        {
            get
            {
                if (currentDatabase != null) return currentDatabase.lang;

                return string.Empty;
            }
        }


        protected override void Awake()
        {
            base.Awake();

            defaultDatabase = _defaultDatabase;
            SetLanguage(defaultDatabase);
            ResetNoKeyOnDatabases();

            NotifyLanguageDatabasesChanged();
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (Application.isPlaying == false)
            {
                // defaultDatabase = _defaultDatabase;
                SetLanguage(defaultDatabase);

                NotifyLanguageDatabasesChanged();
            }
        }

#endif

        private void ResetNoKeyOnDatabases()
        {
            foreach (var db in _databases)
                if (db != null)
                    db.SetString(NoKeyConstant, string.Empty); // Just in-case someone wrote to it.
        }

        private static void NotifyLanguageDatabasesChanged()
        {
            UpdateAvailableLanguageNames();
        }

        private static void UpdateAvailableLanguageNames()
        {
            var l = new List<string>();
            if (instance != null)
                foreach (var db in instance._databases)
                {
                    if (db == null) continue;

                    l.Add(db.lang);
                }

            _availableLanguageNames = l.ToArray();
        }

        public static string[] GetAvailableLanguageNames()
        {
            return _availableLanguageNames;
        }

        public static LocalizationDatabase[] GetAvailableLanguageDatabases()
        {
            if (instance != null) return instance._databases;

            return new LocalizationDatabase[0];
        }

        public static LocalizationDatabase FindLocalizationDatabaseByName(string lang)
        {
            if (instance == null) return null;

            for (var i = 0; i < instance._databases.Length; i++)
                if (instance._databases[i] != null && instance._databases[i].lang == lang)
                    return instance._databases[i];

            return null;
        }

        public static void SetLanguage(string lang)
        {
            currentDatabase = FindLocalizationDatabaseByName(lang);
            if (currentDatabase == null)
                DevdogLogger.LogWarning("Couldn't find localization database with language identifier \"" + lang +
                                        "\"");
        }

        public static void SetLanguage(LocalizationDatabase db)
        {
            currentDatabase = db;
        }


        public static string GetString(string key, string notFound = "")
        {
            if (currentDatabase != null && currentDatabase.ContainsString(key))
                return currentDatabase.GetString(key, notFound);

            if (defaultDatabase != null) return defaultDatabase.GetString(key, notFound);

            return notFound;
        }

        public static T GetObject<T>(string key, T notFound = null) where T : Object
        {
            if (currentDatabase != null && currentDatabase.ContainsObject(key))
                return currentDatabase.GetObject(key, notFound);

            if (defaultDatabase != null) return defaultDatabase.GetObject(key, notFound);

            return notFound;
        }

        /// <summary>
        ///     Creates the key in the default and current databases. The default database is the fallback, so should have the key
        ///     also.
        /// </summary>
        public static string CreateNewStringKey(string defaultValue = "")
        {
            Assert.IsNotNull(defaultDatabase, "The default database HAS to be set if you want to create a new key.");

            var key = Guid.NewGuid().ToString();
            defaultDatabase.SetString(key, defaultValue);
            if (currentDatabase != null) currentDatabase.SetString(key, defaultValue);

            return key;
        }


        /// <summary>
        ///     Creates the key in the default and current databases. The default database is the fallback, so should have the key
        ///     also.
        /// </summary>
        public static string CreateNewObjectKey<T>(T defaultValue = null) where T : Object
        {
            Assert.IsNotNull(defaultDatabase, "The default database HAS to be set if you want to create a new key.");

            var key = Guid.NewGuid().ToString();
            defaultDatabase.SetObject(key, defaultValue);
            if (currentDatabase != null) currentDatabase.SetObject(key, defaultValue);

            return key;
        }
    }
}