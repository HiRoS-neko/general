﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Devdog.General
{
    public class BetterMonoBehaviour : MonoBehaviour, ISerializationCallbackReceiver
    {
        /// <summary>
        ///     Used to store the state of this scriptable object.
        ///     Fetched through reflection to avoid people messing with it.
        /// </summary>
        [SerializeField]
        [IgnoreCustomSerialization] // Ignore in custom serializer - Let unity seriarlize this.
        private string _serializedJsonString = "{}";

        [SerializeField]
        [IgnoreCustomSerialization] // Ignore in custom serializer - Let unity seriarlize this.
        private List<Object> _objectReferences;

        [NonSerialized]
        private readonly BetterSerializationModel _serializer = new();

        public void OnBeforeSerialize()
        {
            Save();
        }

        public void OnAfterDeserialize()
        {
            Load();
        }

        public virtual void Save()
        {
            _serializer.Save(ref _objectReferences, ref _serializedJsonString, this);
        }

        public virtual void Load()
        {
            _serializer.Load(ref _objectReferences, ref _serializedJsonString, this);
        }
    }
}