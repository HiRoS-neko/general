﻿using Devdog.General.UI;
using UnityEngine;

namespace Devdog.General
{
    public class Trigger : TriggerBase
    {
        /// <summary>
        ///     Only required if handling the window directly
        /// </summary>
        [Header("The window")]
        [SerializeField]
        private UIWindowField _window;

        [Header("Animations & Audio")]
        public MotionInfo useAnimation = new();

        public MotionInfo unUseAnimation = new();

        public AudioClipInfo useAudioClip = new();
        public AudioClipInfo unUseAudioClip = new();

        protected Animator animator;
        protected ITriggerWindowContainer windowContainer;

        public bool handleWindowDirectly
        {
            get
            {
                if (windowContainer != null && windowContainer.Equals(null) == false) return false;

                return true;
            }
        }

        public UIWindowField window
        {
            get => _window;
            set => _window = value;
        }

        /// <summary>
        ///     The window this trigger will use;
        ///     If a ITriggerWindowContainer is present it will grab it's window, if not the UIWindowField (this.window) will be
        ///     used.
        /// </summary>
        public UIWindow windowToUse
        {
            get
            {
                if (windowContainer != null)
                    return windowContainer.window;

                return window.window;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            animator = GetComponent<Animator>();
            windowContainer = GetComponent<ITriggerWindowContainer>();
        }

        protected virtual void WindowOnHide()
        {
            DoUnUse(PlayerManager.instance.currentPlayer);
        }

        protected virtual void WindowOnShow()
        {
        }

        public override void DoVisuals()
        {
            if (useAnimation.motion != null && animator != null) animator.Play(useAnimation);

            AudioManager.AudioPlayOneShot(useAudioClip);
        }

        public override void UndoVisuals()
        {
            if (unUseAnimation.motion != null && animator != null) animator.Play(unUseAnimation);

            AudioManager.AudioPlayOneShot(unUseAudioClip);
        }

        private void SubscribeToWindowEvents(UIWindow window)
        {
            if (window != null)
            {
                window.OnShow += WindowOnShow;
                window.OnHide += WindowOnHide;

                if (handleWindowDirectly) window.Show();
            }
        }

        private void UnSubscribeFromWindowEvents(UIWindow window)
        {
            if (window != null)
            {
                window.OnShow -= WindowOnShow;
                window.OnHide -= WindowOnHide;

                if (handleWindowDirectly) window.Hide();
            }
        }

        public override bool Use(Player player)
        {
            if (CanUse(player) == false) return false;

            if (isInUse) return true;

            DoUse(player);
            return true;
        }

        protected virtual void DoUse(Player player)
        {
            if (TriggerManager.currentActiveTrigger != null) TriggerManager.currentActiveTrigger.UnUse(player);

            SubscribeToWindowEvents(windowToUse);
            DoVisuals();

            TriggerManager.currentActiveTrigger = this;
            NotifyTriggerUsed(player);
        }

        /// <summary>
        ///     Force use this trigger (ignores range and other conditions), and will not set a state (it won't set this as the
        ///     active trigger),
        ///     also UI elements won't be shown (windows)
        ///     <remarks>This method can be useful when you want to let a NPC or something other than the player use a trigger.</remarks>
        /// </summary>
        /// <param name="user">The object that used this trigger, null if not used by an object.</param>
        public virtual void ForceUseWithoutStateAndUI(GameObject user)
        {
            DoVisuals();
            NotifyTriggerUsed(null);
        }

        public override bool UnUse(Player player)
        {
            if (CanUnUse(player) == false) return false;

            DoUnUse(player);
            return true;
        }

        protected virtual void DoUnUse(Player player)
        {
            UnSubscribeFromWindowEvents(windowToUse);
            UndoVisuals();

            TriggerManager.currentActiveTrigger = null;
            NotifyTriggerUnUsed(player);
        }

        public virtual void ForceUnUseWithoutStateAndUI(GameObject user)
        {
            UndoVisuals();
            NotifyTriggerUnUsed(null);
        }
    }
}