using System;
using UnityEngine;
using Orazum.UI;

public static class UIDelegatesContainer
{
    // UIEventsUpdater
    public static Func<UIEventsUpdater> FuncEventsUpdater;

    // Main Menu
    public static Action StartGameButtonPressed;
    public static Func<float> FuncSceneLoadingProgress;

    // Menu
    public static Action EventExitToMainMenuPressed;
    public static Action EventContinueButtonPressed;
}