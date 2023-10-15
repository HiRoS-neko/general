using System;
using Random = UnityEngine.Random;

namespace Devdog.General
{
    [Serializable]
    public struct FMinMaxRange
    {
        public float min;
        public float max;

        public float Generate()
        {
            return Random.Range(min, max);
        }
    }
}