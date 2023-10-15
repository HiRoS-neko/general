using System;

namespace Devdog.General
{
    [AttributeUsage(AttributeTargets.Field)]
    public class HideGroupAttribute : Attribute
    {
        public HideGroupAttribute()
            : this(true)
        {
        }

        public HideGroupAttribute(bool includeArrayChildren)
        {
            this.includeArrayChildren = includeArrayChildren;
        }

        public bool includeArrayChildren { get; protected set; }
    }
}