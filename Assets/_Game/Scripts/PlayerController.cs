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
    
    private (int side, int up) _index;

    private void Awake()
    {
        _sidesKeyCodes = new KeyCode[] { KeyCode.S, KeyCode.Space, KeyCode.W, KeyCode.A, KeyCode.D };
        foreach (BeizerSegment bs in _switchBeizerSegments)
        {
            bs.Initialize();
        }

        _index.side = 0;
        _index.up = 0;
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
        int beizerSegmentIndexToUse = (int)_index.side * 6 + switchType;
        ChangeIndex(switchType);
        
        Quaternion targetRot = transform.rotation;
        Vector3 fromDirection = -transform.position;
        Vector3 toDirection = -_switchBeizerSegments[beizerSegmentIndexToUse].Target;
        Quaternion delta = Quaternion.FromToRotation(fromDirection, toDirection);
        targetRot *= delta;

        StartCoroutine(SwitchSequence(switchType, beizerSegmentIndexToUse, targetRot));
    }

    private IEnumerator SwitchSequence(int switchType, int beizerSegmentIndex, Quaternion targetRot)
    {
        GameDelegatesContainer.EventSwitchSideStart.Invoke(SwitchTypeToSideIndex(switchType));
        _state = StateType.SwitchingSide;

        float lerpParam = 0;
        Vector3 initialPos = transform.position;
        Quaternion initialRot = transform.rotation;
        while (lerpParam < 1)
        {
            lerpParam += _switchSpeedLerp * Time.deltaTime;
            if (lerpParam > 1)
            {
                lerpParam = 1;
            }

            transform.position = _switchBeizerSegments[beizerSegmentIndex].GetLerpedPos(lerpParam);
            transform.rotation = Quaternion.Slerp(initialRot, targetRot, lerpParam);
            GameDelegatesContainer.EventSwitchSideLerpParam?.Invoke(lerpParam);
            yield return null;
        }

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

    private void ChangeIndex(int switchType)
    {
        int prevUp = _index.up;
        _index.up = PlayerLookUpTables.UpChange[_index.side, prevUp, switchType];
        _index.side = PlayerLookUpTables.SideConnections[_index.side, prevUp, switchType];
    }
}