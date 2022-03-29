using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace HexedHeroes.Utils
{
    public class JsonCharacterCreator : MonoBehaviour
    {
        [SerializeField] private CharacterConfig characterConfig;
        [SerializeField] private List<CharacterConfig> characterList;
    
        [ContextMenu("LogCharacterAsJson")]
        public void LogCharacterAsJson()
        {
            string characterListJson = JsonConvert.SerializeObject(characterList);
            Debug.Log(characterListJson);
        }
    
        [ContextMenu("SetCharacter")]
        public void SetCharacter()
        {
            characterList.Add(characterConfig);
        }
    }
}
