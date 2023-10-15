using System;

namespace Devdog.General
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IgnoreCustomSerializationAttribute : Attribute
    {
    }
}