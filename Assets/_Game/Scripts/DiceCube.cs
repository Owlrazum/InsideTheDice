using UnityEngine;
using UnityEngine.Assertions;

public enum SideType
{ 
    Bottom,
    Top,
    Backward,
    Forward,
    Left,
    Right
}

public class DiceCube : MonoBehaviour
{
    private Transform[] _sides;

    private const int BOTTOM = 0;
    private const int TOP = 1;
    private const int BACKWARD = 2;
    private const int FORWARD = 3;
    private const int LEFT = 4;
    private const int RIGHT = 5;

    void Awake()
    {
        _sides = new Transform[6];
        for (int i = 0; i < 6; i++)
        { 
            _sides[i] = transform.GetChild(i);
        }

        GameDelegatesContainer.EventSwitchSideStart -= OnSwitchSide;
        GameDelegatesContainer.FuncSidePos += GetSidePos;
    }

    private void OnDestroy()
    { 
        GameDelegatesContainer.EventSwitchSideStart -= OnSwitchSide;
        GameDelegatesContainer.FuncSidePos -= GetSidePos;
    }

    private void OnSwitchSide(int sideIndex)
    {
        Assert.AreNotEqual(sideIndex, 2);
        switch (sideIndex)
        { 
            case 0:
                OnBottomSideSwitch();
                break;
            case 1:
                OnTopSideSwitch();
                break;
            case 3:
                OnForwardSideSwitch();
                break;
            case 4:
                OnLeftSideSwitch();
                break;
            case 5:
                OnRightSideSwitch();
                break;
        }
    }

    private Vector3 GetSidePos(SideType side)
    {
        return _sides[SideToIndex(side)].position;
    }

    private void OnBottomSideSwitch()
    {
        Transform t      = _sides[BOTTOM];
        _sides[BOTTOM]   = _sides[FORWARD];
        _sides[FORWARD]  = _sides[TOP];
        _sides[TOP]      = _sides[BACKWARD];
        _sides[BACKWARD] = t;
    }

    private void OnTopSideSwitch()
    {
        Transform t      = _sides[TOP];
        _sides[TOP]      = _sides[FORWARD];
        _sides[FORWARD]  = _sides[BOTTOM];
        _sides[BOTTOM]   = _sides[BACKWARD];
        _sides[BACKWARD] = t;
    }

    private void OnForwardSideSwitch()
    {
        Transform t      = _sides[FORWARD];
        _sides[FORWARD]  = _sides[BACKWARD];
        _sides[BACKWARD] = t;

        t                = _sides[BOTTOM];
        _sides[BOTTOM]   = _sides[TOP];
        _sides[TOP]      = t;
    }

    private void OnLeftSideSwitch()
    {
        Transform t      = _sides[LEFT];
        _sides[LEFT]     = _sides[FORWARD];
        _sides[FORWARD]  = _sides[RIGHT];
        _sides[RIGHT]    = _sides[BACKWARD];
        _sides[BACKWARD] = t;
    }

    private void OnRightSideSwitch()
    {
        Transform t      = _sides[RIGHT];
        _sides[RIGHT]     = _sides[FORWARD];
        _sides[FORWARD]  = _sides[LEFT];
        _sides[LEFT]    = _sides[BACKWARD];
        _sides[BACKWARD] = t;
    }

    private int SideToIndex(SideType side)
    {
        return (int)side;
    }
}