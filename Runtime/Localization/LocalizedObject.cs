using System;
using Object = UnityEngine.Object;

namespace Devdog.General.Localization
{
    [Serializable]
    public class LocalizedObject : LocalizedObjectBase<Object>
    {
        public LocalizedObject()
        {
        }

        public LocalizedObject(string key)
            : base(key)
        {
        }
    }
}