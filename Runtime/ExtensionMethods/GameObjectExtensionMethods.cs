﻿using UnityEngine;

namespace Devdog.General
{
    public static class GameObjectExtensionMethods
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var a = gameObject.GetComponent<T>();
            return a != null ? a : gameObject.AddComponent<T>();
        }
    }
}