using UnityEngine;

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
    private DiceSide[] _diceSides;

    private const int BOTTOM = 0;
    private const int TOP = 1;
    private const int BACKWARD = 2;
    private const int FORWARD = 3;
    private const int LEFT = 4;
    private const int RIGHT = 5;

    private SideType _currentSide;

    void Awake()
    {
        _diceSides = new DiceSide[6];
        for (int i = 0; i < 6; i++)
        { 
            bool isFound = transform.GetChild(0).GetChild(i).TryGetComponent(out _diceSides[i]);
#if UNITY_EDITOR
            if (!isFound)
            {
                Debug.LogError("Children should be diceSides!");
            }
#endif 
        }

        _currentSide = SideType.Backward;

        GameDelegatesContainer.EventSwitchSideEnd += OnSwitchSideEnd;
        
        GameDelegatesContainer.FuncDoesCurrentSideHasAnySymbols += GetCurrentSideDotCount;
        InputDelegatesContainer.SideSymbolPlacement += OnSideSymbolPlacement;
        GameDelegatesContainer.FuncCurrentSidePos += GetCurrentSidePos;
        GameDelegatesContainer.FuncCurrentSideInsidePos += GetCurrentSideInsidePos;
        GameDelegatesContainer.FuncCurrentSideOutsidePos += GetCurrentSideOutsidePos;

    }

    private void OnDestroy()
    { 
        GameDelegatesContainer.EventSwitchSideEnd -= OnSwitchSideEnd;

        GameDelegatesContainer.FuncDoesCurrentSideHasAnySymbols -= GetCurrentSideDotCount;
        InputDelegatesContainer.SideSymbolPlacement -= OnSideSymbolPlacement;
        GameDelegatesContainer.FuncCurrentSidePos -= GetCurrentSidePos;
        GameDelegatesContainer.FuncCurrentSideInsidePos -= GetCurrentSideInsidePos;
        GameDelegatesContainer.FuncCurrentSideOutsidePos -= GetCurrentSideOutsidePos;

    }

    private void OnSwitchSideEnd(SideType newSide)
    {
        _currentSide = newSide;
    }

    private Vector3 GetCurrentSidePos()
    {
        return _diceSides[(int)_currentSide].Position;
    }

    private Vector3 GetCurrentSideInsidePos()
    {
        return _diceSides[(int)_currentSide].InsidePosition;
    }

    private Vector3 GetCurrentSideOutsidePos()
    {
        return _diceSides[(int)_currentSide].OutsidePosition;
    }

    private int GetCurrentSideDotCount()
    {
        return _diceSides[(int)_currentSide].DotCount;
    }

    private void OnSideSymbolPlacement(int dotCount)
    {
        _diceSides[(int)_currentSide].DotCount = dotCount;
    }
}