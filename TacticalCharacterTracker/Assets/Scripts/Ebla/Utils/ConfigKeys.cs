namespace Ebla.Utils
{
    public static class ConfigKeys
    {
        #region Base Keys

        public const string NAME_KEY = "Name";
        public const string DESCRIPTION_KEY = "Description";

        #endregion
        
        #region Dungeon Keys

        public const string ENCOUNTERS_KEY = "Encounters";

        #endregion

        #region Encounter Keys

        public const string ENEMY_TYPES_KEY = "EnemyTypes";
        public const string ENEMY_KEY = "Enemy";
        public const string QUANTITY_KEY = "Quantity";
        
        #endregion

        #region Character Keys

        public const string HEALTH_KEY = "Health";
        public const string DEFENSE_KEY = "Defense";
        public const string SPEED_KEY = "Speed";
        public const string ABILITIES_KEY = "Abilities";

        #endregion

        #region Ability Keys

        public const string COOLDOWN_KEY = "CooldownTurns";
        public const string PASSIVE_KEY = "IsPassive";
        public const string INTERRUPT_KEY = "IsInterrupt";

        #endregion
    }
}
