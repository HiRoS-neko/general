﻿using UnityEngine.Assertions;

namespace Devdog.General
{
    public class GeneralSettingsManager : ManagerBase<GeneralSettingsManager>
    {
        [Required]
        public GeneralSettings settings;

        protected override void Awake()
        {
            base.Awake();

            settings.defaultCursor.Enable();
            Assert.raiseExceptions = settings.useExceptionsForAssertions;
            DevdogLogger.minimaLog = settings.minimalLogType;
        }
    }
}