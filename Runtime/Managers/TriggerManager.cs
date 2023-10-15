using System;

namespace Devdog.General
{
    public class TriggerManager : ManagerBase<TriggerManager>
    {
        private static TriggerBase _currentActiveTrigger;

        public static TriggerBase currentActiveTrigger
        {
            get => _currentActiveTrigger;
            set
            {
                var before = _currentActiveTrigger;
                _currentActiveTrigger = value;
                if (before != _currentActiveTrigger)
                    if (OnCurrentTriggerChanged != null)
                        OnCurrentTriggerChanged(before, _currentActiveTrigger);
            }
        }

        public static event Action<TriggerBase, TriggerBase> OnCurrentTriggerChanged;
    }
}