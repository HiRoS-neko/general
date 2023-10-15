using System;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace Devdog.General.Editors
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomObjectPickerAttribute : Attribute
    {
        public CustomObjectPickerAttribute(Type type)
            : this(type, 0)
        {
        }

        public CustomObjectPickerAttribute(Type type, int priority)
        {
            this.type = type;
            this.priority = priority;

            Assert.IsTrue(typeof(Object).IsAssignableFrom(type),
                "Given type " + type.Name + " does not inherit from UnityEngine.Object.");
        }

        public int priority { get; protected set; }
        public Type type { get; protected set; }
    }
}