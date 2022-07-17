using System.Collections;
using UnityEngine;

public enum GameStateType
{
    MainMenu,
    MainMenuTransition,
    Transition,
    CubeTurns,
    InsideCube
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
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        GameDelegatesContainer.FuncGameState -= GetGameState;

        UIDelegatesContainer.StartGameButtonPressed -= OnGameStartButtonPressed;
        
        InputDelegatesContainer.CubeTurnsCommand -= OnCubeTurnsCommand;
        GameDelegatesContainer.EventTransitionToInsideCubeEnd -= OnTransitionToInsideCubeEnd;
        GameDelegatesContainer.EventTransitionToCubeTurnsEnd -= OnTransitionToCubeTurnsEnd;
    }

    private void Start()
    {
        ApplicationDelegatesContainer.ShouldStartLoadingNextScene();
    }

    private GameStateType GetGameState()
    {
        return _gameState;
    }

    private void OnGameStartButtonPressed()
    {
        ApplicationDelegatesContainer.EventBeforeLoadingNextScene();
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
        else
        { 
            _gameState = GameStateType.CubeTurns;
            ApplicationDelegatesContainer.LevelCubeTurnsStart(_gameDescription.Levels[_currentLevel]);
        }
    }

    private void OnTransitionToInsideCubeEnd()
    {
        _gameState = GameStateType.InsideCube;
        ApplicationDelegatesContainer.LevelInsideCubeStart();
    }
}