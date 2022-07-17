using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private int _sceneIndexToTest;
#endif

    private AsyncOperation _loadingScene;

    private void Awake()
    {
        ApplicationDelegatesContainer.ShouldStartLoadingNextScene += StartLoadingNextScene;
        ApplicationDelegatesContainer.ShouldFinishLoadingNextScene += FinishLoadingScene;

        UIDelegatesContainer.FuncSceneLoadingProgress += GetSceneLoadingProgress;
    }

    private void OnDestroy()
    { 
        ApplicationDelegatesContainer.ShouldStartLoadingNextScene -= StartLoadingNextScene;
        ApplicationDelegatesContainer.ShouldFinishLoadingNextScene -= FinishLoadingScene;

        UIDelegatesContainer.FuncSceneLoadingProgress -= GetSceneLoadingProgress;
    }

    private void StartLoadingNextScene(int sceneIndex)
    { 
        _loadingScene = SceneManager.LoadSceneAsync(sceneIndex);
        _loadingScene.allowSceneActivation = false;
        ApplicationDelegatesContainer.EventStartedLoadingNextScene?.Invoke();
    }

    private float GetSceneLoadingProgress()
    {
        return _loadingScene.progress;
    }

    private void FinishLoadingScene()
    { 
        _loadingScene.allowSceneActivation = true;
    }
}