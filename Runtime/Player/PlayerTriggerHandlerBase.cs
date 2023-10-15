using System;
using System.Collections.Generic;
using UnityEngine;

namespace Devdog.General
{
    public abstract class PlayerTriggerHandlerBase<T> : MonoBehaviour, IPlayerTriggerHandler where T : Component
    {
        [SerializeField]
        private BestTriggerSelectorBase _selector;

        private TriggerBase _selectedTrigger;

        protected PlayerTriggerHandlerBase()
        {
            triggersInRange = new List<TriggerBase>();
        }

        public Player player { get; set; }

        protected virtual void Awake()
        {
            gameObject.layer = 2;

            InvokeRepeating("UpdateSelectedTrigger", 0f, 0.2f);
        }

        protected virtual void Update()
        {
            if (selectedTrigger != null)
            {
                var input = selectedTrigger.GetComponent<ITriggerInputHandler>();
                if (input != null && input.AreKeysDown())
                {
                    input.Use();
                    selectedTrigger =
                        null; // Clear it in case the trigger use removes the object. If not the next cycle will find the best trigger again.
                    UpdateSelectedTrigger();
//                    selectedTrigger.Toggle();
                }
            }
            else
            {
                UpdateSelectedTrigger();
            }
        }

        public event Action<TriggerBase, TriggerBase> OnSelectedTriggerChanged;

        public BestTriggerSelectorBase selector
        {
            get => _selector;
            set => _selector = value;
        }

        public TriggerBase selectedTrigger
        {
            get => _selectedTrigger;
            protected set
            {
                var before = _selectedTrigger;
                _selectedTrigger = value;
                if (before != _selectedTrigger)
                    if (OnSelectedTriggerChanged != null)
                        OnSelectedTriggerChanged(before, _selectedTrigger);
            }
        }

        public List<TriggerBase> triggersInRange { get; protected set; }

        public virtual bool IsInRangeOfTrigger(TriggerBase trigger)
        {
            return triggersInRange.Contains(trigger);
        }

        protected virtual void UpdateSelectedTrigger()
        {
            if (selector == null) return;

            selectedTrigger = selector.GetBestTrigger(player, triggersInRange);
        }

        protected void NotifyTriggerEnter(T other)
        {
            var c = other.GetComponentInChildren<TriggerBase>();
            if (c != null && (c.rangeHandler == null || c.rangeHandler.Equals(null)))
            {
                triggersInRange.Add(c);
                c.NotifyCameInRange(player);
            }
        }

        protected void NotifyTriggerExit(T other)
        {
            var c = other.GetComponentInChildren<TriggerBase>();
            if (c != null && (c.rangeHandler == null || c.rangeHandler.Equals(null)))
            {
                c.NotifyWentOutOfRange(player);
                triggersInRange.Remove(c);
            }
        }
    }
}