﻿using UnityEngine;

namespace Devdog.General
{
    public interface IPoolable
    {
        GameObject gameObject { get; }
        Transform transform { get; }

        void ResetStateForPool();
    }
}