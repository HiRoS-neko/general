using System;
using Object = UnityEngine.Object;

namespace Devdog.General
{
    [Serializable]
    public class Asset<T> : IAsset where T : Object
    {
        public T val;

        public Asset()
        {
        }

        public Asset(T val)
        {
            this.val = val;
        }

        public Object objectVal
        {
            get => val;
            set => val = (T)value;
        }
    }

    public interface IAsset
    {
        Object objectVal { get; set; }
    }
}