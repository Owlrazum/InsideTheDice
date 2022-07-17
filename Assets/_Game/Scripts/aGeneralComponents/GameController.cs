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

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    { 
        UIDelegatesContainer.StartGameButtonPressed -= OnGameStartButtonPressed;
    }

    private void OnGameStartButtonPressed()
    {
        ApplicationDelegatesContainer.ShouldLoadNextScene?.Invoke();
        ApplicationDelegatesContainer.ShouldPrepareLevel?.Invoke(_gameDescription.GameSequence[_currentLevel]);
        _currentLevel++;
    }
}