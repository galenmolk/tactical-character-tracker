using UnityEngine;

public abstract class Ability : GameElement
{
    [TextArea(4, 20)] public string description;

    public abstract string CooldownText { get; }
}
