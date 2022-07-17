using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameDesciptionSO _gameDescription;

    private int _currentLevel = 0;

    private void Awake()
    {
        UIDelegatesContainer.StartGameButtonPressed += OnGameStartButtonPressed;
        GameDelegatesContainer.TransitionToInsideCubeEnd += OnPlayerCubeTurnsReady;

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        UIDelegatesContainer.StartGameButtonPressed -= OnGameStartButtonPressed;
        GameDelegatesContainer.TransitionToInsideCubeEnd -= OnPlayerCubeTurnsReady;;
    }

    private void Start()
    {
        ApplicationDelegatesContainer.ShouldStartLoadingNextScene();
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
        print("Start");
        ApplicationDelegatesContainer.LevelCubeTurnsStart(_gameDescription.Levels[_currentLevel]);
        _currentLevel++;
    }

    private void OnPlayerCubeTurnsReady()
    { 
    }
}