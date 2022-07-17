using UnityEngine;

public class UIEndLevelCanvas : UIBaseFadingCanvas
{
    [SerializeField]
    private UIButton _returnToMainMenuButton;

    protected override void Awake()
    {
        base.Awake();

        _returnToMainMenuButton.EventOnTouch += OnReturnMainMenuPressed;

        UIDelegatesContainer.ShowEndLevelCanvas += ShowItself;
    }

    private void OnDestroy()
    { 
        UIDelegatesContainer.ShowEndLevelCanvas -= ShowItself;
        _returnToMainMenuButton.EventOnTouch -= OnReturnMainMenuPressed;
    }

    private void OnReturnMainMenuPressed()
    {
        ApplicationDelegatesContainer.ReturnMainMenuPressed();
    }
}