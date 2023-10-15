using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Devdog.General
{
    public class BetterSerializationModel
    {
        [NonSerialized]
        private bool _isSerializing;

        public void Save(ref List<Object> objectReferences, ref string json, Object obj)
        {
            if (_isSerializing || Application.isPlaying)
                return;

            _isSerializing = true;
            objectReferences =
                new List<Object>(); // Has to be new list, ref type -> Clear will clear it inside the serializer
            json = JsonSerializer.Serialize(obj, obj.GetType(), objectReferences);

#if UNITY_EDITOR
            EditorUtility.SetDirty(obj);
#endif

            _isSerializing = false;
        }

        public void Load(ref List<Object> objectReferences, ref string json, Object obj)
        {
            if (_isSerializing)
                return;

            _isSerializing = true;
            JsonSerializer.DeserializeTo(obj, obj.GetType(), json, objectReferences);
            _isSerializing = false;
        }
    }
}