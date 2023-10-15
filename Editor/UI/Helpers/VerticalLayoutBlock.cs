using System;
using UnityEditor;
using UnityEngine;

namespace Devdog.General.Editors
{
    public class VerticalLayoutBlock : IDisposable
    {
        public VerticalLayoutBlock()
            : this(GUIStyle.none)
        {
        }

        public VerticalLayoutBlock(GUIStyle style)
        {
            EditorGUILayout.BeginVertical(style);
        }

        public void Dispose()
        {
            EditorGUILayout.EndVertical();
        }
    }
}