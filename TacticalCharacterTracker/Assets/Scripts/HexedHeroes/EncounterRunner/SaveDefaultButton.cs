using Ebla.Models;
using HexedHeroes.EncounterRunner;

public class SaveDefaultButton : LockedButton
{
    public void SaveDefault()
    {
        EncounterRunner.Instance.TrySaveDefault();
    }

    protected override bool CanUnlock()
    {
        EncounterConfig config = EncounterRunner.Instance.ActiveConfig;
        return !config.HasDefault || !config.IsDefaultLoaded;
    }
}
