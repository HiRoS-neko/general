using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Devdog.General
{
    public abstract class PoolBase<T> : IEnumerable<T>
    {
        protected PoolBase(T baseObject, int startSize = 32)
        {
            this.baseObject = baseObject;
            this.startSize = startSize;
            activeObjectsList = new List<T>(startSize);
            inactiveObjectsPool = new List<T>(startSize);

            rootObject = new GameObject("_PoolRoot").transform;
            rootObject.transform.SetParent(GeneralSettingsManager.instance.transform);

            Init();
        }

        public List<T> inactiveObjectsPool { get; protected set; }
        public List<T> activeObjectsList { get; protected set; }
        public Transform rootObject { get; protected set; }
        public T baseObject { get; protected set; }
        public int startSize { get; protected set; }

        public IEnumerator<T> GetEnumerator()
        {
            return activeObjectsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void Init()
        {
            for (var i = 0; i < startSize; i++) Instantiate();
        }

        public abstract T Instantiate();

        public abstract T Get(bool createWhenNoneLeft = true);

        public abstract void Destroy(T item);

        public void DestroyAll()
        {
            var c = 0;
            while (activeObjectsList.Count > 0 && c++ < startSize)
                Destroy(activeObjectsList[0]);

//            foreach (var item in inactiveObjectsPool)
//                Destroy(item);
        }
    }
}