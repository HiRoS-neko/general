using System;
using Random = UnityEngine.Random;

namespace Devdog.General
{
    [Serializable]
    public struct MinMaxRange
    {
        public int min;
        public int max;

        public int Generate()
        {
            return Random.Range(min, max);
        }
    }
}