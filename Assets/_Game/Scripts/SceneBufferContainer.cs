using UnityEngine;

public class SceneBufferContainer : MonoBehaviour
{
    private Quaternion _cubeRotation;

    private void Awake()
    {
        ApplicationDelegatesContainer.SceneBufferGetCubeRotation += GetCubeRotation;
        ApplicationDelegatesContainer.SceneBufferSetCubeRotation += StoreCubeRotation;
    }

    private void OnDestroy()
    { 
        ApplicationDelegatesContainer.SceneBufferGetCubeRotation -= GetCubeRotation;
        ApplicationDelegatesContainer.SceneBufferSetCubeRotation -= StoreCubeRotation;
    }

    private void StoreCubeRotation(Quaternion cubeRotationArg)
    {
        _cubeRotation = cubeRotationArg;
    }

    private Quaternion GetCubeRotation()
    {
        return _cubeRotation;
    }
}