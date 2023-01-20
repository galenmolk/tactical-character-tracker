using UnityEngine;

namespace Ebla.Utils
{
    public static class HotKeys
    {
        private const string MAP_ASSET = "Ebla/HotKeyMap";

        public static KeyCode Back => Map.Back;
        public static KeyCode ForceExecute => Map.ForceExecute;
        public static KeyCode ReduceCooldowns => Map.ReduceCooldowns;

        private static HotKeyMap Map
        {
            get
            {
                if (map == null)
                {
                    map = LoadMap();
                }

                return map;
            }
        }

        private static HotKeyMap map;

        private static HotKeyMap LoadMap()
        {
            return Resources.Load(MAP_ASSET, typeof(HotKeyMap)) as HotKeyMap;
        }
    }
}
