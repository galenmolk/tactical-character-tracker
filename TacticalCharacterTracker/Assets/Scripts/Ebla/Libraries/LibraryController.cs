using System.Collections.Generic;
using Ebla.Models;

namespace Ebla.Libraries
{
    public abstract class LibraryController<TConfig> 
        where TConfig : BaseConfig
    {
        private Dictionary<string, TConfig> library = new();

        public void Add(TConfig config)
        {
            library.Add(config.Id, config);
        }

        public void Remove(TConfig config)
        {
            library.Remove(config.Id);
        }

        public Dictionary<string, TConfig> All()
        {
            return library;
        }

        public void LoadInConfigs(Dictionary<string, TConfig> configs)
        {
            library = configs;
        }
    }
}
