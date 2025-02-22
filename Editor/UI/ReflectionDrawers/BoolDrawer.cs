﻿using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Devdog.General.Editors.ReflectionDrawers
{
    public sealed class BoolDrawer : SimpleValueDrawer
    {
        public BoolDrawer(FieldInfo fieldInfo, object value, object parentValue, int arrayIndex)
            : base(fieldInfo, value, parentValue, arrayIndex)
        {
        }

        protected override object DrawInternal(Rect rect)
        {
            return EditorGUI.Toggle(GetSingleLineHeightRect(rect), fieldName, (bool)value);
        }
    }
}