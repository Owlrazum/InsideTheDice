using System;

public static class ApplicationDelegatesContainer
{ 
    public static Action ShouldLoadNextScene;

    public static Action ShouldStartLoadingNextScene;
    public static Action EventStartedLoadingNextScene;

    public static Action<LevelDescriptionSO> ShouldPrepareLevel;

    public static Action ShouldLoadMainMenu;
}