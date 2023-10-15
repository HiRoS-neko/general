using System;
using UnityEngine.Assertions;

namespace Devdog.General.Editors.GameRules
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HidesGameRuleAttribute : Attribute
    {
        public HidesGameRuleAttribute(Type type)
        {
            Assert.IsTrue(typeof(IGameRule).IsAssignableFrom(type), "Given type is not a game rule.");
            this.type = type;
        }

        public Type type { get; protected set; }
    }
}