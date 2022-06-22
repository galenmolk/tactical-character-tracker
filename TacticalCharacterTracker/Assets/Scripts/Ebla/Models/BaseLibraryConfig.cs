using System.Collections.Generic;

namespace Ebla.Models
{
    public abstract class BaseLibraryConfig<TConfig> where TConfig : BaseConfig
    {
        protected BaseLibraryConfig()
        {
            Contents = new List<TConfig>();
        }

        public List<TConfig> Contents { get; }
    }
}
