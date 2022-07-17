using System;

public static class InputDelegatesContainer
{
    public static Action CubeTurnsCommand;

    /// <summary>
    /// Accepts dot count
    /// </summary>
    public static Action<int> SideSymbolPlacement;
}