using System;
using System.Collections.Generic;
using Ebla.Models;
using MolkExtras;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.UI
{
    public static class ConfigParamsRegistry
    {
        private const string ASSET_PATH = "ConfigParams";
        
        private static Dictionary<BaseConfig.Type, ConfigParams> paramsRegistry;

        public static ConfigParams Get<TConfig>(TConfig config) where TConfig : BaseConfig
        {
            paramsRegistry ??= BuildRegistry();
            return paramsRegistry.TryGetValue(config.ConfigType, out ConfigParams configParams) ? configParams : null;
        }

        private static Dictionary<BaseConfig.Type, ConfigParams> BuildRegistry()
        {
            ConfigParams[] configParams = Resources.LoadAll<ConfigParams>(ASSET_PATH);
            Debug.Log("configParams: " + configParams.Length);
            var registry = new Dictionary<BaseConfig.Type, ConfigParams>();
            
            configParams.Iterate(parameters =>
            {
                registry.Add(parameters.Type, parameters);
            });
            
            return registry;
        }
    }
}
