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

        ApplicationDelegatesContainer.LevelCubeTurnsStart += OnLevelCubeTurnsStart;

        _cubeTurnsCamera.Priority = 0;
        _insideCubeCamera.Priority = 1;
    }

    private void OnDestroy()
    {
        ApplicationDelegatesContainer.LevelCubeTurnsStart -= OnLevelCubeTurnsStart;
    }

    private void OnLevelCubeTurnsStart(LevelDescriptionSO desc)
    {
        StartCoroutine(OnNextFrameStartBlend());
    }

    private IEnumerator OnNextFrameStartBlend()
    {
        yield return null;
        _insideCubeCamera.Priority = 0;
        _cubeTurnsCamera.Priority = 1;
    }

    private void Update()
    { 
        print(_brain.IsBlending);
    }

    private Ray GetCameraScreenPointToRay(Vector3 screenPos)
    {
        return _renderingCamera.ScreenPointToRay(screenPos);
    }
}
