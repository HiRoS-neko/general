﻿using System.Collections.Generic;
using UnityEngine;

namespace Devdog.General
{
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour
    {
        public BestTriggerSelectorBase triggerSelector;

        private readonly List<IPlayerInputCallbacks> _playerInputCallbacks = new();
        public IPlayerTriggerHandler triggerHandler;

        protected virtual void Awake()
        {
            SetTriggerHandler();

            if (PlayerManager.instance != null) PlayerManager.instance.currentPlayer = this;
        }

        protected virtual void Start()
        {
            InputManager.OnPlayerInputChanged += OnPlayerInputChanged;
        }

        protected virtual void OnDestroy()
        {
            InputManager.OnPlayerInputChanged -= OnPlayerInputChanged;
        }

        protected virtual void OnPlayerInputChanged(bool isInputLimited)
        {
            GetComponentsInChildren(true, _playerInputCallbacks);
            foreach (var c in _playerInputCallbacks) c.SetInputActive(!isInputLimited);
        }

        protected virtual void SetTriggerHandler()
        {
            var obj = new GameObject("_TriggerHandler");
            obj.transform.SetParent(transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;

            var handler = obj.AddComponent<PlayerTriggerHandler>();
            handler.player = this;
            handler.selector = triggerSelector;

            triggerHandler = handler;
        }
    }
}