using HexedHeroes.Utils;
using UnityEngine;

namespace HexedHeroes.Creator
{
    public class CharacterPanel : MainPanel<CharacterPanel>
    {
        [SerializeField] private CanvasGroup canvasGroup;
    
        public override void Open()
        {
            Debug.Log("Open");
            Close();
            DungeonPanel.Instance.Close();
            CharacterDisplay.Instance.Open();
            canvasGroup.SetIsActive(true);
        }

        public override void Close()
        {
            CharacterDisplay.Instance.Close();
            CharacterEditor.Instance.Close();
            AbilitySelector.Instance.Close();
            canvasGroup.SetIsActive(false);
        }

        protected override void OnAwake()
        {
            Close();
        }
    }
}