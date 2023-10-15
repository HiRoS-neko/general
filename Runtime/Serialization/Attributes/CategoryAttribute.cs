using System;

namespace Devdog.General
{
    [AttributeUsage(AttributeTargets.All)]
    public class CategoryAttribute : Attribute
    {
        public CategoryAttribute(string category)
        {
            this.category = category;
        }

        public string category { get; }

        public override string ToString()
        {
            return category;
        }
    }
}