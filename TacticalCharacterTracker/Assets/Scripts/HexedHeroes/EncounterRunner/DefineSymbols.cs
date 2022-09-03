using System.Collections.Generic;

public static class DefineSymbols
{
    public static readonly List<string> All = new()
    {
        QA_BUILD
    };
    
    public const string QA_BUILD = "QA_BUILD";
}
