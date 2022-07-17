using System;
using UnityEngine;

public static class GameDelegatesContainer
{
    public static Func<GameStateType> FuncGameState;

    public static Action EventTransitionToInsideCubeEnd;
    public static Action EventTransitionToCubeTurnsEnd;

    public static Func<Vector3> FuncPlayerPos;
    public static Func<PlayerStateType> FuncPlayerState;

    public static Func<int> FuncCurrentSideDotCount;
    public static Func<int, bool> FuncIsDotCountAlreadyPlaced;

    public static Func<Vector3> FuncCurrentSidePos;
    public static Func<Vector3> FuncCurrentSideInsidePos;
    public static Func<Vector3> FuncCurrentSideOutsidePos;

    public static Action<SwitchType> EventSwitchSideStart;
    public static Action<float> EventSwitchSideLerpParam;
    public static Action<SwitchType> EventSwitchSideEnd;

    public static Action EventAllDotsArePlaced;
    public static Action EventCubeArrivedAtDestination;
}