using UnityEngine;

public class MainMenuCanvas : MonoBehaviour
{
    private UIButton _startGameButton;
    private UIButton _exitGameButton;

    private void Awake()
    {
        transform.GetChild(0).TryGetComponent(out _startGameButton);
        _startGameButton.EventOnTouch += OnStartGameButtonPressed;

        transform.GetChild(1).TryGetComponent(out _exitGameButton);
        _exitGameButton.EventOnTouch += OnExitGameButtonPressed;
    }

    private void OnDestroy()
    {
        _startGameButton.EventOnTouch -= OnStartGameButtonPressed;
        _exitGameButton.EventOnTouch -= OnExitGameButtonPressed;
    }

    private void OnStartGameButtonPressed()
    {
        UIDelegatesContainer.StartGameButtonPressed?.Invoke();
    }

    private void OnExitGameButtonPressed()
    {
        Application.Quit(0);
    }
}