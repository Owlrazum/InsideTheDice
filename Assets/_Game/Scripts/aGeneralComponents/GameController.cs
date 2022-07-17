using System.Collections;
using UnityEngine;

public enum GameStateType
{
    MainMenu,
    MainMenuTransition,
    Transition,
    CubeTurns,
    InsideCube,
    FinishingLevel,
    Finished
}

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameDesciptionSO _gameDescription;

    
    private GameStateType _gameState;

    private int _currentLevel = 0;

    private void Awake()
    {
        GameDelegatesContainer.FuncGameState += GetGameState;

        UIDelegatesContainer.StartGameButtonPressed += OnGameStartButtonPressed;

        InputDelegatesContainer.CubeTurnsCommand += OnCubeTurnsCommand;
        GameDelegatesContainer.EventTransitionToInsideCubeEnd += OnTransitionToInsideCubeEnd;
        GameDelegatesContainer.EventTransitionToCubeTurnsEnd += OnTransitionToCubeTurnsEnd;

        GameDelegatesContainer.EventAllDotsArePlaced += OnAllDotsArePlaced;

        GameDelegatesContainer.EventCubeArrivedAtDestination += OnCubeArrivedAtDestination;

        ApplicationDelegatesContainer.ReturnMainMenuPressed += OnReturnMainMenuPressed;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        GameDelegatesContainer.FuncGameState -= GetGameState;

        UIDelegatesContainer.StartGameButtonPressed -= OnGameStartButtonPressed;
        
        InputDelegatesContainer.CubeTurnsCommand -= OnCubeTurnsCommand;
        GameDelegatesContainer.EventTransitionToInsideCubeEnd -= OnTransitionToInsideCubeEnd;
        GameDelegatesContainer.EventTransitionToCubeTurnsEnd -= OnTransitionToCubeTurnsEnd;

        GameDelegatesContainer.EventAllDotsArePlaced -= OnAllDotsArePlaced;

        GameDelegatesContainer.EventCubeArrivedAtDestination -= OnCubeArrivedAtDestination;

        ApplicationDelegatesContainer.ReturnMainMenuPressed -= OnReturnMainMenuPressed;
    }

    private void Start()
    {
        ApplicationDelegatesContainer.ShouldStartLoadingNextScene(1);
    }

    private GameStateType GetGameState()
    {
        return _gameState;
    }

    private void OnGameStartButtonPressed()
    {
        ApplicationDelegatesContainer.EventBeforeLoadingGameScene();
        ApplicationDelegatesContainer.ShouldFinishLoadingNextScene();
        
        StartCoroutine(OnNextFrameStartLevel());
    }

    private IEnumerator OnNextFrameStartLevel()
    { 
        yield return null;
        yield return null;
        _gameState = GameStateType.MainMenuTransition;
        ApplicationDelegatesContainer.ShouldStartTransitionToCubeTurns();
    }

    private void OnCubeTurnsCommand()
    {
        if (_gameState == GameStateType.CubeTurns)
        {
            _gameState = GameStateType.Transition;
            ApplicationDelegatesContainer.ShouldStartTransitionToInsideCube();
        }
        else if (_gameState == GameStateType.InsideCube)
        {
            _gameState = GameStateType.Transition;
            ApplicationDelegatesContainer.ShouldStartTransitionToCubeTurns();
        }
    }

    private void OnTransitionToCubeTurnsEnd()
    {
        print(_gameState);
        if (_gameState == GameStateType.MainMenuTransition)
        { 
            _gameState = GameStateType.CubeTurns;
            ApplicationDelegatesContainer.MainMenuLevelStart(_gameDescription.Levels[_currentLevel]);
        }
        else if (_gameState == GameStateType.Transition)
        { 
            _gameState = GameStateType.CubeTurns;
            ApplicationDelegatesContainer.LevelCubeTurnsStart(_gameDescription.Levels[_currentLevel]);
        }
        else if (_gameState == GameStateType.FinishingLevel)
        {
            ApplicationDelegatesContainer.LevelFinish();
        }
    }

    private void OnTransitionToInsideCubeEnd()
    {
        _gameState = GameStateType.InsideCube;
        ApplicationDelegatesContainer.LevelInsideCubeStart();
    }

    private void OnAllDotsArePlaced()
    {
        _gameState = GameStateType.FinishingLevel;
        ApplicationDelegatesContainer.ShouldStartTransitionToCubeTurns();
    }

    private void OnCubeArrivedAtDestination()
    {
        _gameState = GameStateType.Finished;
        UIDelegatesContainer.ShowEndLevelCanvas();
        ApplicationDelegatesContainer.ShouldStartLoadingNextScene(2);
    }

    private void OnReturnMainMenuPressed()
    {
        _gameState = GameStateType.MainMenu;
        ApplicationDelegatesContainer.ShouldFinishLoadingNextScene();
    }

    private IEnumerator OnNextFrameStartLoadingOnceMore()
    { 
        yield return null;
        yield return null;

        ApplicationDelegatesContainer.ShouldStartLoadingNextScene(1);
    }
}