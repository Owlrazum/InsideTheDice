using System.Collections;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Camera), typeof(CinemachineBrain))]
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _cubeTurnsCamera;

    [SerializeField]
    private CinemachineVirtualCamera _insideCubeCamera;

    private Camera _renderingCamera;
    private CinemachineBrain _brain;

    private void Awake()
    {
        TryGetComponent(out _renderingCamera);
        TryGetComponent(out _brain);

        ApplicationDelegatesContainer.ShouldStartTransitionToCubeTurns += StartTransitionToCubeTurns;
        ApplicationDelegatesContainer.ShouldStartTransitionToInsideCube += StartTransitionToInsideCube;

        _cubeTurnsCamera.Priority = 0;
        _insideCubeCamera.Priority = 1;
    }

    private void OnDestroy()
    {
        ApplicationDelegatesContainer.ShouldStartTransitionToCubeTurns -= StartTransitionToCubeTurns;
        ApplicationDelegatesContainer.ShouldStartTransitionToInsideCube -= StartTransitionToInsideCube;
    }

    private void StartTransitionToCubeTurns()
    {
        _insideCubeCamera.Priority = 0;
        _cubeTurnsCamera.Priority = 1;
        StartCoroutine(NotifyTransitionToCubeTurnsEnd());
        // StartCoroutine(OnNextFrameStartBlend());
    }

    private IEnumerator NotifyTransitionToCubeTurnsEnd()
    {
        print(_brain.m_DefaultBlend.BlendTime);
        yield return new WaitForSeconds(_brain.m_DefaultBlend.BlendTime);
        GameDelegatesContainer.EventTransitionToCubeTurnsEnd();
    }

    private IEnumerator OnNextFrameStartBlend()
    {
        yield return null;
    }

    private void StartTransitionToInsideCube()
    {
        _cubeTurnsCamera.Priority = 0;
        _insideCubeCamera.Priority = 1;
        StartCoroutine(NotifyTransitionToInsideCubeEnd());
    }

    private IEnumerator NotifyTransitionToInsideCubeEnd()
    {
        yield return new WaitForSeconds(_brain.m_DefaultBlend.BlendTime);
        GameDelegatesContainer.EventTransitionToInsideCubeEnd();
    }

    private Ray GetCameraScreenPointToRay(Vector3 screenPos)
    {
        return _renderingCamera.ScreenPointToRay(screenPos);
    }
}
