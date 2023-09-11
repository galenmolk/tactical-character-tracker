using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HexedHeroes.Player
{
    public class CharacterSelect : MonoBehaviour
    {
        [SerializeField] private CharacterButton characterButtonPrefab;
        [SerializeField] private RectTransform characterButtonParent;
        [SerializeField] private GameObject offlineWarning;

        protected void Awake()
        {
            if (!ActiveSession.IsOnline)
            {
                offlineWarning.SetActive(true);
            }

            if (ActiveSession.AvailableCharacters == null)
                return;

            PopulateCharacterButtons(ActiveSession.AvailableCharacters);
        }

        private void SelectAndLoadCharacter(CharacterConfig characterConfig)
        {
            ActiveSession.SelectedCharacter = characterConfig;
            SceneManager.LoadScene(SceneKeys.CHARACTER_SHEET);
        }

        private void PopulateCharacterButtons(List<CharacterConfig> characterList)
        {
            for (int i = 0, length = characterList.Count; i < length; i++)
            {
                CharacterButton button = Instantiate(characterButtonPrefab, characterButtonParent);
                button.Initialize(characterList[i], SelectAndLoadCharacter);
            }
        }
    }
}
