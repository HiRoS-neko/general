﻿using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Devdog.General.Editors.ReflectionDrawers
{
    public sealed class AnimationCurveDrawer : SimpleValueDrawer
    {
        public AnimationCurveDrawer(FieldInfo fieldInfo, object value, object parentValue, int arrayIndex)
            : base(fieldInfo, value, parentValue, arrayIndex)
        {
        }

        protected override object DrawInternal(Rect rect)
        {
            return EditorGUI.CurveField(GetSingleLineHeightRect(rect), fieldName, (AnimationCurve)value);
        }
    }
}