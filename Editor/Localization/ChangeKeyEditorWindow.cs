using System;
using UnityEditor;
using UnityEngine;

namespace Devdog.General.Localization.Editors
{
    internal class ChangeKeyEditorWindow : EditorWindow
    {
        private const string TextFieldKey = "LocalizedKeyTextField";
        private string _key = "";
        private Action<string> _keyRenameCallback;

        protected void OnEnable()
        {
//            GUI.FocusControl(TextFieldKey);
        }

        protected void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();

            GUI.SetNextControlName(TextFieldKey);
            _key = EditorGUILayout.TextField(_key);
            GUI.FocusControl(TextFieldKey);

            if (GUILayout.Button("Save")) Save(_key, _keyRenameCallback);

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
                Save(_key, _keyRenameCallback);

            EditorGUILayout.EndHorizontal();
        }

        public static void ShowWindow(string key, Action<string> keyRenameCallback)
        {
            var w = GetWindow<ChangeKeyEditorWindow>();
            w.minSize = new Vector2(300, 60);
            w.maxSize = new Vector2(300, 60);
            w.titleContent = new GUIContent("Localization Editor");
            w._key = key;
            w._keyRenameCallback = keyRenameCallback;
            w.OnEnable();
            w.ShowUtility();
        }

        private void Save(string key, Action<string> callback)
        {
            callback(key);
            Close();
        }
    }
}