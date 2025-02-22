﻿using System;

namespace Devdog.General
{
    /// <summary>
    ///     When used this field will show in inside the node, as well as the properties sidebar.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class SummaryAttribute : Attribute
    {
        public SummaryAttribute(string summary)
        {
            this.summary = summary;
        }

        public string summary { get; private set; }
    }
}