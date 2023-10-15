using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Devdog.General
{
    public class InputManager : ManagerBase<InputManager>
    {
        protected static List<GameObject> limitUIInputTo = new(8);
        protected static List<GameObject> limitPlayerInputTo = new(8);
        public static event Action<bool> OnUIInputLimitChanged;
        public static event Action<bool> OnPlayerInputChanged;

        private static bool CanReceiveInput(GameObject obj, GameObject limitObject)
        {
            if (obj == null)
                return false;

            if (obj.activeInHierarchy == false)
                return false;

            // Input is not limited to single object.
            if (limitObject == null)
                return true;

            // Input is limited to object, object or all children are allowed to deliver input.
            if (obj == limitObject || obj.transform.IsChildOf(limitObject.transform))
                return true;

            // Obj was not a child or match, don't allow input.
            return false;
        }

        #region UI Input

        private static void NotifyUIInputChanged(bool b)
        {
            OnUIInputLimitChanged?.Invoke(b);
        }

        public static void LimitUIInputTo(GameObject obj)
        {
            limitUIInputTo.Add(obj);
            NotifyUIInputChanged(true);
        }

        public static void RemoveUILimitInput(GameObject obj)
        {
            var last = limitUIInputTo.LastOrDefault();
            limitUIInputTo.Remove(obj);

            if (limitUIInputTo.Count == 0)
                NotifyUIInputChanged(false);
            else if (IsUIInputLimitedTo(last)) NotifyUIInputChanged(true);
        }

        public static void ClearUIInputLimits()
        {
            limitUIInputTo.Clear();
            NotifyUIInputChanged(false);
        }

        public static GameObject GetLimitedUIInputObject()
        {
            if (limitUIInputTo.Count > 0) return limitUIInputTo[^1];

            return null;
        }

        public static bool IsUIInputLimitedTo(GameObject obj)
        {
            return GetLimitedUIInputObject() == obj;
        }

        public static bool CanReceiveUIInput(GameObject obj)
        {
            return CanReceiveInput(obj, GetLimitedUIInputObject());
        }

        #endregion

        #region Player input

        private static void NotifyPlayerInputChanged(bool b)
        {
            OnPlayerInputChanged?.Invoke(b);
        }

        public static void LimitPlayerInputTo(GameObject obj)
        {
            limitPlayerInputTo.Add(obj);
            NotifyPlayerInputChanged(true);
        }

        public static void RemovePlayerLimitInput(GameObject obj)
        {
            var last = limitPlayerInputTo.LastOrDefault();
            limitPlayerInputTo.Remove(obj);

            if (limitPlayerInputTo.Count == 0)
                NotifyPlayerInputChanged(false);
            else if (IsPlayerInputLimitedTo(last)) NotifyPlayerInputChanged(true);
        }

        public static void ClearPlayerInputLimits(GameObject obj)
        {
            limitPlayerInputTo.Clear();
            NotifyPlayerInputChanged(false);
        }

        public static GameObject GetLimitedPlayerInputObject()
        {
            if (limitPlayerInputTo.Count > 0) return limitPlayerInputTo[^1];

            return null;
        }

        public static bool IsPlayerInputLimitedTo(GameObject obj)
        {
            return GetLimitedPlayerInputObject() == obj;
        }

        public static bool CanReceivePlayerInput(GameObject obj)
        {
            return CanReceiveInput(obj, GetLimitedPlayerInputObject());
        }

        #endregion
    }
}