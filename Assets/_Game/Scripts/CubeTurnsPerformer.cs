using System.Collections;
using UnityEngine;

using Orazum.Utilities;

public class CubeTurnsPerformer : MonoBehaviour
{
    [SerializeField]
    private Transform _levelCubeTransform;

    [SerializeField]
    private float _returnToIdentitySpeedLerp = 0.5f;

    [SerializeField]
    private float _turnDistance = 12;

    [SerializeField]
    private float _turnSpeedLerp = 1;

    [SerializeField]
    private float _turnCompleteWaitTime = 0.2f;

    [SerializeField]
    private Vector3 _sequenceCompleteTargetOffset = Vector3.down * 11;

    private CubeTurnType[] _cubeTurnSequence;

    private MeshRenderer _renderer;

    private IEnumerator _cubeTurnsSequence;

    private Vector3 _levelCubePosition;

    private void Awake()
    {
        TryGetComponent(out _renderer);
        ApplicationDelegatesContainer.ShouldStartTransitionToCubeTurns += OnShouldStartTransitionToCubeTurns;

        ApplicationDelegatesContainer.MainMenuLevelStart += OnMainMenuLevelStart;
        ApplicationDelegatesContainer.LevelCubeTurnsStart += OnLevelCubeTurnsStart;
        ApplicationDelegatesContainer.LevelInsideCubeStart += OnLevelInsideCubeStart;

        ApplicationDelegatesContainer.LevelFinish += OnLevelFinish;

        _levelCubePosition = _levelCubeTransform.localPosition;
    }

    private void OnDestroy()
    { 
        ApplicationDelegatesContainer.ShouldStartTransitionToCubeTurns -= OnShouldStartTransitionToCubeTurns;

        ApplicationDelegatesContainer.MainMenuLevelStart -= OnMainMenuLevelStart;
        ApplicationDelegatesContainer.LevelCubeTurnsStart -= OnLevelCubeTurnsStart;
        ApplicationDelegatesContainer.LevelInsideCubeStart -= OnLevelInsideCubeStart;

        ApplicationDelegatesContainer.LevelFinish -= OnLevelFinish;
    }

    private void OnShouldStartTransitionToCubeTurns()
    {
        if (GameDelegatesContainer.FuncGameState() == GameStateType.MainMenuTransition)
        { 
            _renderer.enabled = false;
            Quaternion mainMenuRotation = ApplicationDelegatesContainer.SceneBufferGetCubeRotation();
            _levelCubeTransform.localRotation = mainMenuRotation;
        }
    }

    private void OnMainMenuLevelStart(CubeTurnType[] cubeTurnSequenceArg)
    {
        _cubeTurnSequence = cubeTurnSequenceArg;
        StartCoroutine(ReturnCubeToIdentityRotation());
    }

    private IEnumerator ReturnCubeToIdentityRotation()
    {
        float lerpParam = 0;
        Quaternion initialRotation = _levelCubeTransform.localRotation;
        while (lerpParam < 1)
        { 
            lerpParam += _returnToIdentitySpeedLerp * Time.deltaTime;
            Quaternion lerpRotation = Quaternion.Slerp(initialRotation, Quaternion.identity, lerpParam);;
            _levelCubeTransform.localRotation = lerpRotation;
            yield return null;
        }
        _levelCubeTransform.localRotation = Quaternion.identity;
        OnReturnCubeToIdentityRotationComplete();
    }


    private void OnReturnCubeToIdentityRotationComplete()
    {
        _renderer.enabled = true;
        _cubeTurnsSequence = CubeTurnsSequence();
        StartCoroutine(_cubeTurnsSequence);
    }

    private void OnLevelCubeTurnsStart(CubeTurnType[] cubeTurnSequenceArg)
    {
        _cubeTurnSequence = cubeTurnSequenceArg;
        _renderer.enabled = true;
        _cubeTurnsSequence = CubeTurnsSequence();
        StartCoroutine(_cubeTurnsSequence);
    }

    private IEnumerator CubeTurnsSequence()
    {
        int currentStep = -1;
        float lerpParam = 0;
        while (true)
        {
            Vector3 initialPos = transform.localPosition;
            Vector3 targetPos = initialPos;
            Quaternion initialRot = transform.localRotation;
            Quaternion targetRot = initialRot;

            currentStep++;
            if (currentStep >= _cubeTurnSequence.Length)
            {
                targetPos += _sequenceCompleteTargetOffset;

                lerpParam = 0;
                while (lerpParam < 1)
                {
                    lerpParam += _turnSpeedLerp * Time.deltaTime;
                    transform.localPosition = Vector3.Lerp(initialPos, targetPos, MathUtilities.EaseInOut(lerpParam));
                    transform.localRotation = Quaternion.Slerp(initialRot, targetRot, MathUtilities.EaseInOut(lerpParam));
                    yield return null;
                }

                initialPos = _levelCubePosition;
                targetPos = initialPos;
                transform.localPosition = initialPos;

                yield return new WaitForSeconds(_turnCompleteWaitTime);
                currentStep = 0;
            }

            CubeTurnType currentTurn = _cubeTurnSequence[currentStep];
            
            switch (currentTurn)
            { 
                case CubeTurnType.Backward:
                    targetPos += -Vector3.forward * _turnDistance;
                    targetRot *= Quaternion.Euler(-Vector3.right * 90);
                    break;
                case CubeTurnType.Forward:
                    targetPos += Vector3.forward * _turnDistance;
                    targetRot *= Quaternion.Euler(Vector3.right * 90);
                    break;
                case CubeTurnType.Left:
                    targetPos += -Vector3.right * _turnDistance;
                    targetRot *= Quaternion.Euler(-Vector3.forward * 90);
                    break;
                case CubeTurnType.Right:
                    targetPos += Vector3.right * _turnDistance;
                    targetRot *= Quaternion.Euler(Vector3.forward * 90);
                    break;
            }

            lerpParam = 0;
            while (lerpParam < 1)
            {
                lerpParam += _turnSpeedLerp * Time.deltaTime;
                transform.localPosition = Vector3.Lerp(initialPos, targetPos, MathUtilities.EaseInOut(lerpParam));
                transform.localRotation = Quaternion.Slerp(initialRot, targetRot, MathUtilities.EaseInOut(lerpParam));
                yield return null;
            }

            yield return new WaitForSeconds(_turnCompleteWaitTime);
        }
    }

    private void OnLevelInsideCubeStart()
    {
        StopCoroutine(_cubeTurnsSequence);
        transform.localPosition = _levelCubePosition;
        transform.localRotation = Quaternion.identity;
        _renderer.enabled = false;
    }

    private void OnLevelFinish()
    {
        StartCoroutine(FinishingSequence());
    }

    private IEnumerator FinishingSequence()
    {
        int currentStep = -1;
        float lerpParam = 0;
        while (true)
        {
            Vector3 initialPos = _levelCubeTransform.localPosition;
            Vector3 targetPos = initialPos;
            Quaternion initialRot = _levelCubeTransform.localRotation;
            Quaternion targetRot = initialRot;

            currentStep++;
            if (currentStep >= _cubeTurnSequence.Length)
            {
                GameDelegatesContainer.EventCubeArrivedAtDestination();
                yield break;
            }

            CubeTurnType currentTurn = _cubeTurnSequence[currentStep];
            
            switch (currentTurn)
            { 
                case CubeTurnType.Backward:
                    targetPos += -Vector3.forward * _turnDistance;
                    targetRot *= Quaternion.Euler(-Vector3.right * 90);
                    break;
                case CubeTurnType.Forward:
                    targetPos += Vector3.forward * _turnDistance;
                    targetRot *= Quaternion.Euler(Vector3.right * 90);
                    break;
                case CubeTurnType.Left:
                    targetPos += -Vector3.right * _turnDistance;
                    targetRot *= Quaternion.Euler(-Vector3.forward * 90);
                    break;
                case CubeTurnType.Right:
                    targetPos += Vector3.right * _turnDistance;
                    targetRot *= Quaternion.Euler(Vector3.forward * 90);
                    break;
            }

            lerpParam = 0;
            while (lerpParam < 1)
            {
                lerpParam += _turnSpeedLerp * Time.deltaTime;
                _levelCubeTransform.localPosition = Vector3.Lerp(initialPos, targetPos, MathUtilities.EaseInOut(lerpParam));
                _levelCubeTransform.localRotation = Quaternion.Slerp(initialRot, targetRot, MathUtilities.EaseInOut(lerpParam));
                yield return null;
            }

            yield return new WaitForSeconds(_turnCompleteWaitTime);
        }
    }
}