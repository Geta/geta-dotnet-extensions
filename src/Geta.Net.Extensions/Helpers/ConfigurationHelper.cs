﻿// Copyright (c) Geta Digital. All rights reserved.
// Licensed under Apache-2.0. See the LICENSE file in the project root for more information
using System;
using System.Configuration;

namespace Geta.Net.Extensions.Helpers
{
    public static class ConfigurationHelper
    {
        public static T GetConfig<T>(string prefix, string name, T defaultValue)
        {
            return GetConfigInternal(prefix, name, false, defaultValue);
        }

        public static string GetConfigRequired(string prefix, string name)
        {
            return GetConfigRequired<string>(prefix, name);
        }

        public static T GetConfigRequired<T>(string prefix, string name)
        {
            return GetConfigInternal<T>(prefix, name);
        }

        private static T GetConfigInternal<T>(string prefix, string name, bool required = true, T defaultValue = default(T))
        {
            var configKey = $"{prefix}:{name}";
            var configValueString = ConfigurationManager.AppSettings[configKey];

            if (!string.IsNullOrEmpty(configValueString))
            {
                if (typeof(T).IsEnum)
                    return (T)Enum.Parse(typeof(T), configValueString);

                return (T)Convert.ChangeType(configValueString, typeof(T));
            }

            if (required)
            {
                throw new ConfigurationErrorsException($"{configKey} is required but is not set");
            }

            return defaultValue;
        }
    }
}