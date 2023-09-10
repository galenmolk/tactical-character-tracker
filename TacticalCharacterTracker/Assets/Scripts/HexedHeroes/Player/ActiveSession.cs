using System.Collections.Generic;

namespace HexedHeroes.Player
{
    public static class ActiveSession
    {
        public static bool IsOffline;
        public static CharacterConfig SelectedCharacter;
        public static List<CharacterConfig> AvailableCharacters;
    }
}
