using System;
using System.Collections.Generic;
using Devdog.General.ThirdParty.FullSerializer;
using Devdog.General.ThirdParty.FullSerializer.Internal;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Devdog.General
{
    public static class JsonSerializer
    {
        private static readonly fsSerializer _serializer;
        private static readonly object _lockObject = new();
        private static readonly fsUnityEngineObjectConverter _engineObjectConvertor;

        static JsonSerializer()
        {
            _serializer = new fsSerializer();
            _serializer.Config = new fsConfig
            {
                SerializeEnumsAsInteger = true,
                IgnoreSerializeAttributes = new[]
                {
                    typeof(IgnoreCustomSerializationAttribute),
                    typeof(NonSerializedAttribute),
                    typeof(fsIgnoreAttribute)
                },
                SerializeAttributes = new[]
                {
                    typeof(CustomSerializationAttribute),
                    typeof(SerializeField),
                    typeof(fsPropertyAttribute)
                }
            };
            _serializer.RemoveProcessor<fsSerializationCallbackReceiverProcessor>();

            _engineObjectConvertor = new fsUnityEngineObjectConverter();
            _serializer.AddConverter(_engineObjectConvertor);
        }

        public static Type currentRootType { get; private set; }

        private static void SetObjectReferences(List<Object> objectReferences)
        {
            if (objectReferences != null) _serializer.Context.Set(objectReferences);
        }


        public static string Serialize<T>(T value, List<Object> objectReferences)
        {
            return Serialize(value, typeof(T), objectReferences);
        }

        public static string Serialize(object value, Type type, List<Object> objectReferences)
        {
            lock (_lockObject)
            {
                try
                {
                    fsData data;

                    SetObjectReferences(objectReferences);
                    currentRootType = type;
                    _serializer.TrySerialize(type, value, out data).AssertSuccessWithoutWarnings();

                    return fsJsonPrinter.CompressedJson(data);
                }
                catch (Exception e)
                {
                    Debug.LogError("Couldn't serialize type " + type + " - " + e.Message + "\n" + e.StackTrace);
//                    throw;
                }
            }

            return null;
        }

        public static void DeserializeTo(object obj, Type type, string json, List<Object> objectReferences)
        {
            DeserializeTo(ref obj, type, json, objectReferences);
        }

        public static void DeserializeTo(ref object obj, Type type, string json, List<Object> objectReferences)
        {
            lock (_lockObject)
            {
                try
                {
                    var data = fsJsonParser.Parse(json);
                    SetObjectReferences(objectReferences);
                    currentRootType = type;
                    _serializer.TryDeserialize(data, type, ref obj).AssertSuccessWithoutWarnings();
                }
                catch (Exception e)
                {
                    DevdogLogger.LogError(e.Message + "\n" + e.StackTrace);
//                    throw;
                }
            }
        }

        public static void DeserializeTo<T>(T obj, string json, List<Object> objectReferences)
        {
            DeserializeTo(ref obj, json, objectReferences);
        }

        public static void DeserializeTo<T>(ref T obj, string json, List<Object> objectReferences)
        {
            lock (_lockObject)
            {
                try
                {
                    var data = fsJsonParser.Parse(json);
                    SetObjectReferences(objectReferences);
                    currentRootType = typeof(T);
                    _serializer.TryDeserialize(data, ref obj).AssertSuccessWithoutWarnings();
                }
                catch (Exception e)
                {
                    Debug.LogError("Couldn't deserialize type " + typeof(T) + " - " + e.Message);
//                    throw;
                }
            }
        }
    }
}