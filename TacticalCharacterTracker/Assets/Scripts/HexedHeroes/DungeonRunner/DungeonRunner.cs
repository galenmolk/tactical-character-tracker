using Newtonsoft.Json;
using TMPro;
using UnityEngine;

namespace HexedHeroes.DungeonRunner
{
    public class DungeonRunner : MonoBehaviour
    {
        [SerializeField] private TextAsset dungeonJson;
        [SerializeField] private TMP_Text dungeonTitleText;
        [SerializeField] private EnemyTypeBlock enemyTypeBlockPrefab;
        [SerializeField] private Transform enemyTypeBlockParent;
        
        private DungeonConfig dungeonConfig;
        
        private void Awake()
        {
            dungeonConfig = JsonConvert.DeserializeObject<DungeonConfig>(dungeonJson.text);
            dungeonTitleText.text = dungeonConfig.name;
            CreateEnemyTypes();
        }

        private void CreateEnemyTypes()
        {
            foreach (var enemyTypeConfig in dungeonConfig.enemyTypes)
            {
                EnemyTypeBlock block = Instantiate(enemyTypeBlockPrefab, enemyTypeBlockParent);
                block.Initialize(enemyTypeConfig);
            }
        }
    }
}
