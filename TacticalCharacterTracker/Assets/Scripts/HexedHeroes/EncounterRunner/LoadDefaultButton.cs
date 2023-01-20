using Ebla.Models;
using HexedHeroes.EncounterRunner;

public class LoadDefaultButton : LockedButton
{
    public void LoadDefault()
    {
        EncounterRunner.Instance.TryLoadDefault();
    }

    protected override bool CanUnlock()
    {
        EncounterConfig config = EncounterRunner.Instance.ActiveConfig;
        return config.HasDefault && !config.IsDefaultLoaded;
    }
}
