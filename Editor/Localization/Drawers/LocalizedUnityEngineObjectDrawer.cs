﻿using System.Reflection;
using Devdog.General.Editors.ReflectionDrawers;
using UnityEngine;

namespace Devdog.General.Localization.Editors
{
    [CustomDrawer(typeof(LocalizedObject))]
    public class LocalizedUnityEngineObjectDrawer : LocalizedObjectDrawerBase<Object, LocalizedObject>
    {
        public LocalizedUnityEngineObjectDrawer(FieldInfo fieldInfo, object value, object parentValue, int arrayIndex)
            : base(fieldInfo, value, parentValue, arrayIndex)
        {
        }

        public override DrawerBase FindDrawerRelative(string fieldName)
        {
            return null;
        }
    }
}