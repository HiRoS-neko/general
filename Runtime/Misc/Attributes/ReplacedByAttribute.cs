using System;

namespace Devdog.General
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ReplacedByAttribute : Attribute
    {
        public ReplacedByAttribute(Type type)
        {
            this.type = type;
        }

        public Type type { get; protected set; }
    }
}