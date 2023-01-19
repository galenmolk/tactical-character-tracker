namespace Ebla.Utils
{
    public static class ConfigKeys
    {
        #region Base Keys

        public const string NAME_KEY = "Name";
        public const string DESCRIPTION_KEY = "Description";
        public const string ID_KEY = "Id";
        public const string PATH_KEY = "Path";
        public const string FULL_PATH_KEY = "FullPath";
        
        #endregion
        
        #region Dungeon Keys

        public const string ENCOUNTERS_KEY = "Encounters";

        #endregion

        #region Encounter Keys

        public const string ENEMIES = "Enemies";
        public const string DEFAULT_ENEMIES = "Default";
        public const string IS_DEFAULT_LOADED = "IsDefaultLoaded";

        #endregion

        #region Enemy Type Keys

        public const string ENEMY_KEY = "Enemy";
        public const string QUANTITY_KEY = "Quantity";
        
        #endregion

        #region Character Keys

        public const string HEALTH_KEY = "Health";
        public const string DEFENSE_KEY = "Defense";
        public const string SPEED_KEY = "Speed";
        public const string ABILITIES_KEY = "Abilities";
        public const string CHARACTER_INSTANCES_KEY = "CharacterInstances";

        #endregion

        #region Enemy Instance Keys

        public const string CURRENT_HEALTH_KEY = "CurrentHealth";
        public const string CURRENT_DEFENSE_KEY = "CurrentDefense";
        public const string ABILITY_INSTANCES_KEY = "AbilityInstances";

        #endregion

        #region Ability Keys

        public const string COOLDOWN_KEY = "CooldownTurns";
        public const string PASSIVE_KEY = "IsPassive";
        public const string INTERRUPT_KEY = "IsInterrupt";

        #endregion

        #region Ability Instance Keys

        public const string CURRENT_COOLDOWN_TURNS_KEY = "CurrentCooldownTurns";
        public const string IS_ACTIVATED_KEY = "IsActivated";

        #endregion
    }
}
