using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ebla.Models
{
    public abstract class BaseLibraryConfig<TConfig> where TConfig : BaseConfig
    {
        public BaseLibraryConfig()
        {
            Contents = new List<TConfig>();
        }

        public List<TConfig> Contents { get; private set; }
    }
}
