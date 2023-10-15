using UnityEngine;

namespace Devdog.General.Localization
{
    public abstract class LocalizedObjectBase<T> : ILocalizedObject<T>
        where T : Object
    {
        [SerializeField]
        private readonly string _key;

        protected LocalizedObjectBase()
        {
        }

        protected LocalizedObjectBase(string key)
        {
            _key = key;
        }

        public Object objectVal
        {
            get => val;
            set => val = (T)value;
        }

        /// <summary>
        ///     Gets the message in the currently selected language.
        /// </summary>
        [IgnoreCustomSerialization]
        public T val
        {
            get => LocalizationManager.GetObject<T>(_key);
            set
            {
                if (LocalizationManager.currentDatabase != null)
                    LocalizationManager.currentDatabase.SetObject(_key, value);
            }
        }

        public override string ToString()
        {
            if (objectVal == null) return "null";

            return objectVal.ToString();
        }
    }
}