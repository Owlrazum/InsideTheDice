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


    public static Action<LevelDescriptionSO> LevelCubeTurnsStart;
    public static Action LevelCubeTurnsEnd;
}