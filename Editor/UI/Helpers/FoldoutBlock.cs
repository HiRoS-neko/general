using System;
using Devdog.General.Editors.ReflectionDrawers;
using UnityEditor;
using UnityEngine;

namespace Devdog.General.Editors
{
    public class FoldoutBlock : IDisposable
    {
        private readonly DrawerBase _drawer;

        public FoldoutBlock(DrawerBase drawer, Rect rect, GUIContent content)
        {
            _drawer = drawer;

            isUnFolded = EditorGUI.Foldout(rect, isUnFolded, content);
        }

        public bool isUnFolded
        {
            get => FoldoutBlockUtility.IsUnFolded(_drawer);
            set => FoldoutBlockUtility.Set(_drawer, value);
        }

        public void Dispose()
        {
        }
    }
}