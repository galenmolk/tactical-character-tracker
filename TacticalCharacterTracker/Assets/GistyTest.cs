using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gisty;
using HexedHeroes;
using HexedHeroes.Models;
using UnityEngine;

public class GistyTest : MonoBehaviour
{
    GistService characterService;
    GistService abilityService;
    private void Start()
    {
        characterService = new GistService("12953c4c94be8897997bc3746fd82be9", "characters.json", "hexed-heroes-character-tracker-app");
        abilityService = new GistService("e081087e30ca9af5f1bfbeff95511c3a", "abilities.json", "hexed-heroes-character-tracker-app");

        Test();
    }

    private async void Test()
    {

        var abs = await abilityService.GetGistContent<AbilityConfig[]>();
        var chars = await characterService.GetGistContent<CharacterConfig[]>();
        foreach (var c in chars)
        {
            List<AbilityConfig> abilityConfigs = new();
            foreach (var id in c.abilityIds)
            {
                AbilityConfig a = abs.First(a => string.Equals(a.id, id));
                Debug.Log($"C ({c.name}), A ({a.name})");
                abilityConfigs.Add(a);
            }
            c.abilities = abilityConfigs;
        }
    }
}
