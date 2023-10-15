using System;

namespace Devdog.General
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ArrayControlOptionsAttribute : Attribute
    {
        public bool canAddItems = true;

//        public bool includeArrayChildren { get; protected set; }
        public bool canRemoveItems = true;
    }
}