﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Devdog.General.ThirdParty.FullSerializer.Internal
{
    /// <summary>
    ///     Serializes and deserializes enums by their current name.
    /// </summary>
    public class fsEnumConverter : fsConverter
    {
        public override bool CanProcess(Type type)
        {
            return type.Resolve().IsEnum;
        }

        public override bool RequestCycleSupport(Type storageType)
        {
            return false;
        }

        public override bool RequestInheritanceSupport(Type storageType)
        {
            return false;
        }

        public override object CreateInstance(fsData data, Type storageType)
        {
            // In .NET compact, Enum.ToObject(Type, Object) is defined but the overloads like
            // Enum.ToObject(Type, int) are not -- so we get around this by boxing the value.
            return Enum.ToObject(storageType, (object)0);
        }

        public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
        {
            if (Serializer.Config.SerializeEnumsAsInteger)
            {
                serialized = new fsData(Convert.ToInt64(instance));
            }
            else if (fsPortableReflection.GetAttribute<FlagsAttribute>(storageType) != null)
            {
                var instanceValue = Convert.ToInt64(instance);
                var result = new StringBuilder();

                var first = true;
                foreach (var value in Enum.GetValues(storageType))
                {
                    var integralValue = Convert.ToInt64(value);
                    var isSet = (instanceValue & integralValue) != 0;

                    if (isSet)
                    {
                        if (first == false) result.Append(",");
                        first = false;
                        result.Append(value);
                    }
                }

                serialized = new fsData(result.ToString());
            }
            else
            {
                serialized = new fsData(Enum.GetName(storageType, instance));
            }

            return fsResult.Success;
        }

        public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
        {
            if (data.IsString)
            {
                var enumValues = data.AsString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                long instanceValue = 0;
                for (var i = 0; i < enumValues.Length; ++i)
                {
                    var enumValue = enumValues[i];

                    // Verify that the enum name exists; Enum.TryParse is only available in .NET 4.0
                    // and above :(.
                    if (ArrayContains(Enum.GetNames(storageType), enumValue) == false)
                        return fsResult.Fail("Cannot find enum name " + enumValue + " on type " + storageType);

                    var flagValue = (long)Convert.ChangeType(Enum.Parse(storageType, enumValue), typeof(long));
                    instanceValue |= flagValue;
                }

                instance = Enum.ToObject(storageType, (object)instanceValue);
                return fsResult.Success;
            }

            if (data.IsInt64)
            {
                var enumValue = (int)data.AsInt64;

                // In .NET compact, Enum.ToObject(Type, Object) is defined but the overloads like
                // Enum.ToObject(Type, int) are not -- so we get around this by boxing the value.
                instance = Enum.ToObject(storageType, (object)enumValue);

                return fsResult.Success;
            }

            return fsResult.Fail("EnumConverter encountered an unknown JSON data type");
        }

        /// <summary>
        ///     Returns true if the given value is contained within the specified array.
        /// </summary>
        private static bool ArrayContains<T>(T[] values, T value)
        {
            // note: We don't use LINQ because this function will *not* allocate
            for (var i = 0; i < values.Length; ++i)
                if (EqualityComparer<T>.Default.Equals(values[i], value))
                    return true;

            return false;
        }
    }
}