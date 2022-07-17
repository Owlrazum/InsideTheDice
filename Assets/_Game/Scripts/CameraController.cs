using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    private Camera _renderingCamera;

    private void Awake()
    {
        TryGetComponent(out _renderingCamera);

        GameDelegatesContainer.EventSwitchSideStart += OnSwitchSideStart;
        GameDelegatesContainer.EventSwitchSideLerpParam += OnSwitchSideLerpParam;
    }

    private void OnDestroy()
    {
        GameDelegatesContainer.EventSwitchSideStart -= OnSwitchSideStart;
        GameDelegatesContainer.EventSwitchSideLerpParam -= OnSwitchSideLerpParam;
    }

    private void OnSwitchSideStart(int side)
    { 

    }

    private void OnSwitchSideLerpParam(float lerpParam)
    { 

    }

    private Ray GetCameraScreenPointToRay(Vector3 screenPos)
    {
        return _renderingCamera.ScreenPointToRay(screenPos);
    }
}
