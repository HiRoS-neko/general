using System;

namespace Devdog.General.Editors.ReflectionDrawers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomDrawerAttribute : Attribute
    {
        public CustomDrawerAttribute(Type type)
            : this(type, false)
        {
        }

        public CustomDrawerAttribute(Type type, bool onlyForRoot)
        {
            this.type = type;
            this.onlyForRoot = onlyForRoot;
        }

        public Type type { get; protected set; }
        public bool onlyForRoot { get; protected set; }
    }
}