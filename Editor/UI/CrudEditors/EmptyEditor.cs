using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Devdog.General.Editors
{
    public class EmptyEditor : IEditorCrud
    {
        public List<IEditorCrud> childEditors = new(8);

        public int toolbarIndex;

        public EmptyEditor(string name, EditorWindow window)
        {
            this.name = name;
            this.window = window;
            toolbarIndex = 0;
            requiresDatabase = false;
        }

        public IEditorCrud selectedEditor => childEditors[toolbarIndex];

        public string[] editorNames
        {
            get
            {
                var names = new string[childEditors.Count];
                for (var i = 0; i < childEditors.Count; i++) names[i] = childEditors[i].ToString();

                return names;
            }
        }

        public string name { get; set; }
        public EditorWindow window { get; protected set; }
        public bool requiresDatabase { get; set; }

        public virtual void Focus()
        {
            //if (selectedEditor != null)
            selectedEditor.Focus();
        }

        /// <summary>
        ///     Empty editor only draws child options
        /// </summary>
        public virtual void Draw()
        {
            DrawToolbar();

            for (var i = 0; i < childEditors.Count; i++)
                if (childEditors[i] == selectedEditor)
                    childEditors[i].Draw();
        }

        protected virtual void DrawToolbar()
        {
            if (toolbarIndex >= childEditors.Count || toolbarIndex < 0)
                return;

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();

            var maxHorizontalTabs = Mathf.FloorToInt(window.position.width / 220);
            maxHorizontalTabs = Mathf.Max(2, maxHorizontalTabs);
            for (var i = 0; i < childEditors.Count; i++)
            {
                var buttonType = "LargeButtonMid";
                if (i == 0 || i % maxHorizontalTabs == 0)
                    buttonType = "LargeButtonLeft";
                else if (i == childEditors.Count - 1 || i % maxHorizontalTabs == maxHorizontalTabs - 1)
                    buttonType = "LargeButtonRight";

                if (i == toolbarIndex)
                    GUI.color = Color.gray;

                if (i % maxHorizontalTabs == 0)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.Space();
                }

                if (GUILayout.Button(editorNames[i], buttonType))
                {
                    toolbarIndex = i;
                    selectedEditor.Focus();
                }

                GUI.color = Color.white;
            }

            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        public override string ToString()
        {
            return name;
        }
    }
}