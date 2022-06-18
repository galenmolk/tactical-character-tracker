using System;

namespace Ebla
{
    public static class FileFactory
    {
        public enum FileType
        {
            Dungeon = 0,
            Encounter = 1,
            Enemy = 2,
            Ability = 3
        }
        
        public static IFile GetFileForType(FileType fileType)
        {
            return fileType switch
            {
                FileType.Dungeon => new DungeonConfig(),
                FileType.Encounter => new EncounterConfig(),
                FileType.Enemy => new EnemyConfig(),
                FileType.Ability => new AbilityConfig(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
