using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Devdog.General.Editors.ReflectionDrawers
{
    public class DictionaryDrawer : DrawerBase, IChildrenDrawer
    {
        protected DerivedTypeInformation derivedTypes = new();
        protected OnlyDerivedTypesAttribute onlyDerivedTypes;

        public DictionaryDrawer(FieldInfo fieldInfo, object value, object parentValue, int arrayIndex)
            : base(fieldInfo, value, parentValue, arrayIndex)
        {
            onlyDerivedTypes =
                (OnlyDerivedTypesAttribute)fieldInfo.GetCustomAttributes(typeof(OnlyDerivedTypesAttribute), true)
                    .FirstOrDefault();

            children = new List<DrawerBase>();
            Update();
        }

        public List<DrawerBase> children { get; set; }

        protected override int GetHeightInternal()
        {
            return children.Sum(o => o.GetHeight()) + 20;
        }

        protected override object DrawInternal(Rect rect)
        {
//            GUI.Label(rect, "Dictionary drawer");
//
//            // TODO: Create drawer for dictionary
//            rect.y += ReflectionDrawerStyles.singleLineHeight;
//            if (GUI.Button(rect, "Add string var"))
//            {
//                var dict = (IDictionary)value;
//                dict.Add("Player input", new Variable<string>("abc"));
//            }

            return value;
        }

        public override DrawerBase FindDrawerRelative(string fieldName)
        {
            return null;
        }


        public void Update()
        {
            children.Clear();

            if (value == null)
            {
                value = Activator.CreateInstance(fieldInfo.FieldType);
                NotifyValueChanged(value);
            }

//            var dict = (IDictionary)value;
//            foreach (var val in dict)
//            {
////                var kvp = (DictionaryEntry) val;
////                var child = ReflectionDrawerUtility.BuildEditorHierarchy(fieldInfo, kvp.Key);
////                children.Add(child);
//
////                var child2 = ReflectionDrawerUtility.BuildEditorHierarchy(fieldInfo, kvp.Value);
////                children.Add(child2);
//            }
        }
    }
}