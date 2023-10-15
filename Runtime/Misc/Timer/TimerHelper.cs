using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Devdog.General
{
    public class TimerHelper : MonoBehaviour, ITimerHelper
    {
        [NonSerialized]
        private int _IDCounter;

        [NonSerialized]
        private readonly Dictionary<int, Coroutine> _lookups = new();

        public virtual void StopTimer(int id)
        {
            if (_lookups.ContainsKey(id) == false)
                //                QuestLogger.LogWarning("No timer with that ID exists or is already stopped.");
                return;

            StopCoroutine(_lookups[id]);
            _lookups.Remove(id);

//            QuestLogger.LogVerbose("Auto removed timer with ID: " + id + " (timer completed)");
        }


        public int StartTimer(float time, Action callbackWhenTimeIsOver)
        {
            return StartTimer(time, null, callbackWhenTimeIsOver);
        }

        public virtual int StartTimer(float time, Action callbackContinous, Action callbackWhenTimeIsOver)
        {
            _IDCounter++;
            _lookups[_IDCounter] =
                StartCoroutine(_StartTimer(_IDCounter, time, callbackContinous, callbackWhenTimeIsOver));

            return _IDCounter;
        }

        public void Dispose()
        {
            StopAllTimers();
            Destroy(gameObject);
        }

        public virtual void StopAllTimers()
        {
            StopAllCoroutines();
            _lookups.Clear();
        }

        protected virtual IEnumerator _StartTimer(int timerID, float time, Action callbackContinous,
            Action callbackWhenTimeIsOver)
        {
            var timer = 0f;
            while (timer < time)
            {
                timer += Time.deltaTime;
                if (callbackContinous != null) callbackContinous();

                yield return null;
            }

            _lookups.Remove(timerID);
            if (callbackWhenTimeIsOver != null) callbackWhenTimeIsOver();
        }
    }
}