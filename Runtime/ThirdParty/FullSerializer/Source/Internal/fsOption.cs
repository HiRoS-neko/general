﻿using System;

namespace Devdog.General.ThirdParty.FullSerializer.Internal
{
    /// <summary>
    ///     Simple option type. This is akin to nullable types.
    /// </summary>
    public struct fsOption<T>
    {
        private readonly T _value;

        public bool HasValue { get; }

        public bool IsEmpty => HasValue == false;

        public T Value
        {
            get
            {
                if (IsEmpty) throw new InvalidOperationException("fsOption is empty");
                return _value;
            }
        }

        public fsOption(T value)
        {
            HasValue = true;
            _value = value;
        }

        public static fsOption<T> Empty;
    }

    public static class fsOption
    {
        public static fsOption<T> Just<T>(T value)
        {
            return new fsOption<T>(value);
        }
    }
}