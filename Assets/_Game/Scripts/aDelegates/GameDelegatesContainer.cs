using System;
using UnityEngine;

public static class GameDelegatesContainer
{
    public static Func<GameStateType> FuncGameState;

    public static Action EventTransitionToInsideCubeEnd;
    public static Action EventTransitionToCubeTurnsEnd;

    public static Func<Vector3> FuncCurrentSidePos;
    public static Func<Vector3> FuncCurrentSideInsidePos;
    public static Func<Vector3> FuncCurrentSideOutsidePos;
    
    public static Func<int> FuncDoesCurrentSideHasAnySymbols;

    public static Action<SwitchType> EventSwitchSideStart;
    public static Action<float> EventSwitchSideLerpParam;
    public static Action<SideType> EventSwitchSideEnd;

}