using System;
using UnityEngine;

public static class ApplicationDelegatesContainer
{ 
    public static Action ShouldLoadNextScene;

    public static Action ShouldStartLoadingNextScene;
    public static Action EventStartedLoadingNextScene;
    public static Action ShouldFinishLoadingNextScene;

    public static Action EventBeforeLoadingNextScene;

    public static Action ShouldLoadMainMenu;

    public static Func<Quaternion> SceneBufferGetCubeRotation;
    public static Action<Quaternion> SceneBufferSetCubeRotation;

    public static Action<LevelDescriptionSO> MainMenuLevelStart;

    // TODO move it to game delegates
    public static Action ShouldStartTransitionToCubeTurns;
    public static Action ShouldStartTransitionToInsideCube;

    public static Action<LevelDescriptionSO> LevelCubeTurnsStart;
    public static Action LevelInsideCubeStart;
    public static Action LevelCubeTurnsEnd;
}