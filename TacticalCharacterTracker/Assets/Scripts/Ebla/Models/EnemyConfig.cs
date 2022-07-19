using System;

namespace Ebla.Models
{
    [Serializable]
    public class EnemyConfig : CharacterConfig
    {
        public override Type ConfigType => Type.Enemy;

        public override string BaseName => "Untitled Enemy";
    }
}
