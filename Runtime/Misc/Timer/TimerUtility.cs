using UnityEngine;

namespace Devdog.General
{
    public static class TimerUtility
    {
        private static ITimerHelper _root;
        private static GameObject _obj;

        static TimerUtility()
        {
        }

        public static ITimerHelper GetTimer()
        {
            if (_root == null || _obj == null)
            {
                _obj = new GameObject("_QuestTimer");
#if UNITY_EDITOR
                if (Application.isEditor && Application.isPlaying == false)
                    _root = new FakeTimerHelper();
                else
                    _root = _obj.AddComponent<TimerHelper>();
#else
                _root = _obj.AddComponent<TimerHelper>();
#endif
            }

            // TODO: Consider pooling this.
            return _root;
        }

//        public static void RecyleTimer(TimerHelper timer)
//        {
//            UnityEngine.Object.Destroy(timer.gameObject);
//        }
    }
}