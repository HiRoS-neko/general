using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using EditorStyles = Devdog.General.Editors.EditorStyles;

namespace Devdog.General.Localization.Editors
{
    public class LocalizationEditorWindow : EditorWindow
    {
        private static LocalizationDatabase[] _dbs = new LocalizationDatabase[0];
        private static string _searchQuery = "";
        private static Vector2 _scrollPos;
        private static string[] _stringKeys = new string[0];
        private static string[] _textureKeys = new string[0];
        private static string[] _spriteKeys = new string[0];
        private static string[] _audioClipKeys = new string[0];
        private static string[] _objectKeys = new string[0];

        private static float _colWidth = 200f;

        private static bool _isSearching => string.IsNullOrEmpty(_searchQuery) == false;

        protected void OnEnable()
        {
            _dbs = LocalizationManager.GetAvailableLanguageDatabases();
            _colWidth = Mathf.Max(200f, _dbs.Length / 4f);

            if (LocalizationManager.defaultDatabase != null)
            {
                _stringKeys = LocalizationManager.defaultDatabase._EditorGetAllStrings().Select(o => o.Key).ToArray();
                _textureKeys = LocalizationManager.defaultDatabase._EditorGetAllObjects()
                    .Where(o => o.Value is Texture2D).Select(o => o.Key).ToArray();
                _spriteKeys = LocalizationManager.defaultDatabase._EditorGetAllObjects().Where(o => o.Value is Sprite)
                    .Select(o => o.Key).ToArray();
                _audioClipKeys = LocalizationManager.defaultDatabase._EditorGetAllObjects()
                    .Where(o => o.Value is AudioClip).Select(o => o.Key).ToArray();
                _objectKeys = LocalizationManager.defaultDatabase._EditorGetAllObjects().Select(o => o.Key).ToArray();
            }
        }

        protected void OnGUI()
        {
            var rect = new Rect(0f, 0f, position.width, 20f);
            GUI.BeginGroup(rect, (GUIStyle)"box");
            EditorGUILayout.BeginHorizontal();
            var keysCount = 0;

            foreach (var db in _dbs)
            {
                keysCount += db._EditorGetAllStrings().Count;

                EditorGUI.BeginChangeCheck();
                EditorGUIUtility.labelWidth = 50f;
                var result = EditorGUILayout.ToggleLeft(db.lang, IsLanguageChecked(db), GUILayout.Width(80f));
                if (EditorGUI.EndChangeCheck()) SetLanguageChecked(db, result);

                EditorGUIUtility.labelWidth = 0f;
            }

            EditorGUILayout.EndHorizontal();
            GUI.EndGroup();
            rect.y += rect.height;

            rect.height = 20f;
            _searchQuery = EditorStyles.SearchBar(rect, _searchQuery, this, _isSearching);
            rect.y += rect.height;

            rect.height = position.height + 100f;
//            rect.height = position.height - rect.height - 20f;
            _scrollPos = GUI.BeginScrollView(rect, _scrollPos,
                new Rect(0, 0, 200f * (_dbs.Length + 1), keysCount * EditorGUIUtility.singleLineHeight), false, true);
            var colIndex = 0;
            foreach (var db in _dbs)
            {
                if (IsLanguageChecked(db) == false) continue;

                EditorGUI.LabelField(
                    new Rect(_colWidth * (colIndex + 1), 0f, _colWidth, EditorGUIUtility.singleLineHeight), db.lang);

                if (colIndex == 0)
                {
                    var indexCounter = 0;
                    indexCounter += DrawKeys<string>(_stringKeys, indexCounter);
                    indexCounter += DrawKeys<Texture>(_textureKeys, indexCounter);
                    indexCounter += DrawKeys<Sprite>(_spriteKeys, indexCounter);
                    indexCounter += DrawKeys<AudioClip>(_audioClipKeys, indexCounter);
                    indexCounter += DrawKeys<Object>(_objectKeys, indexCounter);
                }


                GUI.BeginGroup(
                    new Rect(_colWidth * (colIndex + 1), 0f, _colWidth, keysCount * EditorGUIUtility.singleLineHeight),
                    (GUIStyle)"box");

                var counter2 = 0;
                counter2 += DrawLocalizationDataString(db._EditorGetAllStrings(), _stringKeys, counter2);
                counter2 += DrawLocalizationData<Texture2D>(db._EditorGetAllObjects(), _textureKeys, counter2);
                counter2 += DrawLocalizationData<Sprite>(db._EditorGetAllObjects(), _spriteKeys, counter2);
                counter2 += DrawLocalizationData<AudioClip>(db._EditorGetAllObjects(), _audioClipKeys, counter2);
                counter2 += DrawLocalizationData<Object>(db._EditorGetAllObjects(), _objectKeys, counter2);

                colIndex++;
                GUI.EndGroup();
            }

            GUI.EndScrollView();
        }

        [MenuItem("Tools/Devdog/Localization Editor", false, 0)]
        public static void ShowWindow()
        {
            var w = GetWindow<LocalizationEditorWindow>();
            w.minSize = new Vector2(700, 400);
            w.titleContent = new GUIContent("Localization Editor");
            w.OnEnable();
            w.Show();
        }

        private bool IsLanguageChecked(LocalizationDatabase db)
        {
            return EditorPrefs.GetBool(db.lang + "_Checked", true);
        }

        private void SetLanguageChecked(LocalizationDatabase db, bool result)
        {
            EditorPrefs.SetBool(db.lang + "_Checked", result);
        }

        /// <returns>Returns the amount of keys drawn (increments startCount)</returns>
        private static int DrawKeys<T>(string[] keys, int startCount)
        {
            startCount++;
            EditorGUI.LabelField(
                new Rect(0, startCount * EditorGUIUtility.singleLineHeight, 200f, EditorGUIUtility.singleLineHeight),
                typeof(T).Name, UnityEditor.EditorStyles.boldLabel);
            startCount++;
            foreach (var key in keys)
            {
                GUI.Label(
                    new Rect(0, startCount * EditorGUIUtility.singleLineHeight, 200f,
                        EditorGUIUtility.singleLineHeight), key);
                startCount++;
            }

            return startCount;
        }

        private static int DrawLocalizationDataString(Dictionary<string, string> data, string[] keys, int startCount)
        {
            foreach (var key in keys)
            {
                string val;
                data.TryGetValue(key, out val);
                val = val ?? "";

                if (_isSearching && val.Contains(_searchQuery) == false) continue;

                EditorGUI.BeginChangeCheck();
                val = EditorGUI.TextField(
                    new Rect(0f, startCount * EditorGUIUtility.singleLineHeight + 30f, _colWidth,
                        EditorGUIUtility.singleLineHeight), val);
                if (EditorGUI.EndChangeCheck()) data[key] = val;

                startCount++;
            }

            return startCount;
        }

        private static int DrawLocalizationData<T>(Dictionary<string, Object> data, string[] keys, int startCount)
            where T : Object
        {
            startCount++;
            startCount++;

            foreach (var key in keys)
            {
                Object val;
                data.TryGetValue(key, out val);
                if (_isSearching && val != null && val.name.Contains(_searchQuery) == false) continue;

                if (val is T == false) continue;

                EditorGUI.BeginChangeCheck();
                val = (T)EditorGUI.ObjectField(
                    new Rect(0f, startCount * EditorGUIUtility.singleLineHeight + 30f, _colWidth,
                        EditorGUIUtility.singleLineHeight), val, typeof(T), false);
                if (EditorGUI.EndChangeCheck())
                {
                    data[key] = val;
                    GUI.changed = true;
                }

                startCount++;
            }

            return startCount;
        }
    }
}