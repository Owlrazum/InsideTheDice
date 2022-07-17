using System;
using UnityEngine;

public static class GameDelegatesContainer
{
    public static Action TransitionToInsideCubeEnd;

    public static Func<SideType, Vector3> FuncSidePos;

    public static Action<int> EventSwitchSideStart;
    public static Action<float> EventSwitchSideLerpParam;
    public static Action EventSwitchSideEnd;
}