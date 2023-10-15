using System.Reflection;

namespace Devdog.General.Editors.ReflectionDrawers
{
    public class ValueTypeDrawer : ChildrenValueDrawerBase
    {
        public ValueTypeDrawer(FieldInfo fieldInfo, object value, object parentValue, int arrayIndex)
            : base(fieldInfo, value, parentValue, arrayIndex)
        {
        }
    }
}