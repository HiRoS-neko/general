using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Devdog.General.Editors.ReflectionDrawers
{
    public sealed class LayerMaskDrawer : SimpleValueDrawer
    {
        public LayerMaskDrawer(FieldInfo fieldInfo, object value, object parentValue, int arrayIndex)
            : base(fieldInfo, value, parentValue, arrayIndex)
        {
        }

        protected override object DrawInternal(Rect rect)
        {
            var mask = (LayerMask)value;
            return (LayerMask)EditorGUI.MaskField(GetSingleLineHeightRect(rect), fieldName, mask.value,
                InternalEditorUtility.layers);
        }
    }
}