using System.Reflection;

namespace Devdog.General.Editors.ReflectionDrawers
{
    public class ClassDrawer : ChildrenValueDrawerBase
    {
        public ClassDrawer(FieldInfo fieldInfo, object value, object parentValue, int arrayIndex)
            : base(fieldInfo, value, parentValue, arrayIndex)
        {
        }
    }
}