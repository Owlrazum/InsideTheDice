using System;
using System.Collections;
using UnityEngine;

public enum SwitchType
{ 
    ToBottom  = 0,
    ToTop     = 1,
    ToForward = 2,
    ToLeft    = 3,
    ToRight   = 4
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _switchSpeedLerp;

    private enum StateType
    {
        Idle,
        SwitchingSide
    }
    private StateType _state;

    private KeyCode[] _sidesKeyCodes;

    [SerializeField]
    [Tooltip("size should be equal to sideCount * (sideCount - 1)")]
    private BeizerSegment[] _switchBeizerSegments;

    [SerializeField]
    private Transform _beizerParent;

    private Vector3Int _currentTargetUp;
    private Vector3Int _middleTargetUp;

    private void Awake()
    {
        _sidesKeyCodes = new KeyCode[] { KeyCode.S, KeyCode.Space, KeyCode.W, KeyCode.A, KeyCode.D };
        foreach (BeizerSegment bs in _switchBeizerSegments)
        {
            bs.Initialize();
        }

        _currentTargetUp = ToVector3Int(transform.up);
    }

    private void OnDestroy()
    { 

    }

    private void Update()
    {
        if (_state == StateType.Idle)
        {
            InputUpdate();
        }
    }


    private void InputUpdate()
    { 
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(_sidesKeyCodes[i]))
            {
                StartSwitchingSide(i);
            }
        }
    }

    private void StartSwitchingSide(int switchType)
    {
        // Quaternion targetRot = PlayerLookUpTables.Rotations[_index.side, _index.up];
        switch (switchType)
        { 
            case 0:
                _currentTargetUp = ToVector3Int(Quaternion.Euler(-transform.right * 90) * _currentTargetUp);
                _middleTargetUp  = _currentTargetUp;
                break;
            case 1:
                _currentTargetUp = ToVector3Int(Quaternion.Euler(transform.right * 90) * _currentTargetUp);
                _middleTargetUp  = _currentTargetUp;
                break;
            case 2:
                _currentTargetUp = ToVector3Int(Quaternion.Euler(transform.right * 90) * _currentTargetUp);
                _middleTargetUp  = _currentTargetUp;
                _currentTargetUp = ToVector3Int(Quaternion.Euler(transform.right * 90) * _currentTargetUp);
                break;
        }

        print(_currentTargetUp);
        print(_middleTargetUp);

        StartCoroutine(SwitchSequence(switchType));
    }

    private IEnumerator SwitchSequence(int switchType)
    {
        GameDelegatesContainer.EventSwitchSideStart.Invoke(SwitchTypeToSideIndex(switchType));
        _state = StateType.SwitchingSide;

        float lerpParam = 0;
        while (lerpParam < 1)
        {
            lerpParam += _switchSpeedLerp * Time.deltaTime;

            transform.position = _switchBeizerSegments[switchType].GetLerpedPos(lerpParam);
            if (lerpParam < 0.5f && switchType == 2)
            {
                transform.localRotation = Quaternion.LookRotation(new Vector3(0, 5, 0) - transform.position, _middleTargetUp);
                transform.localEulerAngles = CustomMath.Round(transform.localEulerAngles);
            }
            else
            { 
                transform.localRotation = Quaternion.LookRotation(new Vector3(0, 5, 0) - transform.position, _currentTargetUp);
                transform.localEulerAngles = CustomMath.Round(transform.localEulerAngles);
            }
            GameDelegatesContainer.EventSwitchSideLerpParam?.Invoke(lerpParam);
            yield return null;
        }

        Vector3 toRotate = Vector3.zero;
        switch (switchType)
        { 
            case 0:
                toRotate = -transform.right * 90;
                break;
            case 1:
                toRotate = transform.right * 90;
                break;
            case 2:
                toRotate = transform.up * 180 + transform.forward * 180;
                break;
            case 3:
                toRotate = transform.up * 90;
                break;
            case 4:
                toRotate = -transform.up * 90;
                break;
        }
        print(toRotate);
        _beizerParent.Rotate(toRotate, Space.World);

        GameDelegatesContainer.EventSwitchSideEnd?.Invoke();
        _state = StateType.Idle;
    }

    private int SwitchTypeToSideIndex(int index)
    {
        if (index > 1)
        {
            index++;
        }
        return index;
    }

    private Vector3Int ToVector3Int(Vector3 v)
    {
        return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
    }
}