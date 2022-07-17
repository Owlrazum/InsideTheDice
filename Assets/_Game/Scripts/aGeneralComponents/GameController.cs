using System.Collections;
using UnityEngine;

using Orazum.Utilities;

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
    private CubeTurnType[] _firstPlaythrough;

    [SerializeField]
    private int _minimumTurnCount;

    [SerializeField]
    private int _maximumTurnCount;

    private GameStateType _gameState;

    private int _currentLevel = 0;

    private const string HAS_COMPLETED_AT_LEAST_ONCE_pref = "hasCompletedAtLeastOnce";
    private bool _hasCompletedAtLeastOnce;

    private CubeTurnType[] _generatedSequence;

    private void Awake()
    {
        _hasCompletedAtLeastOnce = PlayerPrefs.GetInt(HAS_COMPLETED_AT_LEAST_ONCE_pref, 0) == 1;
        if (_hasCompletedAtLeastOnce)
        {
            GenerateSequence();
        }

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
        CubeTurnType[] sequenceToUse = _hasCompletedAtLeastOnce ? _generatedSequence : _firstPlaythrough;
        if (_gameState == GameStateType.MainMenuTransition)
        { 
            _gameState = GameStateType.CubeTurns;

            ApplicationDelegatesContainer.MainMenuLevelStart(sequenceToUse);
        }
        else if (_gameState == GameStateType.Transition)
        { 
            _gameState = GameStateType.CubeTurns;
            ApplicationDelegatesContainer.LevelCubeTurnsStart(sequenceToUse);
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
        PlayerPrefs.SetInt(HAS_COMPLETED_AT_LEAST_ONCE_pref, 1);
        
    }

    private void OnReturnMainMenuPressed()
    {
        _gameState = GameStateType.MainMenu;
        ApplicationDelegatesContainer.ShouldFinishLoadingNextScene();
        StartCoroutine(OnNextFrameStartLoadingOnceMore());
    }

    private IEnumerator OnNextFrameStartLoadingOnceMore()
    { 
        yield return null;
        yield return null;

        GenerateSequence();
        ApplicationDelegatesContainer.ShouldStartLoadingNextScene(1);
    }

    private void GenerateSequence()
    {
        int turnsCount = Random.Range(_minimumTurnCount, _maximumTurnCount + 1);
        _generatedSequence = new CubeTurnType[turnsCount];
        int prev = 3;
        for (int i = 0; i < turnsCount; i++)
        {
            int turn = IndexUtilities.RandomRangeWithExlusion(0, 4, 3);
            _generatedSequence[i] = (CubeTurnType)turn;
            prev = turn;
        }
    }
}