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

        InputDelegatesContainer.SideSymbolPlacement += OnSideSymbolPlacement;
        GameDelegatesContainer.FuncCurrentSideDotCount += GetCurrentSideDotCount;
        GameDelegatesContainer.FuncCurrentSidePos += GetCurrentSidePos;

       // GameDelegatesContainer.EventCubeArrivedAtDestination += OnCubeArrivedAtDestination;
    }

    private void OnDestroy()
    { 
        InputDelegatesContainer.SideSymbolPlacement -= OnSideSymbolPlacement;
        GameDelegatesContainer.FuncCurrentSideDotCount -= GetCurrentSideDotCount;
        GameDelegatesContainer.FuncCurrentSidePos -= GetCurrentSidePos;

        // GameDelegatesContainer.EventCubeArrivedAtDestination -= OnCubeArrivedAtDestination;
    }

    private void OnSideSymbolPlacement(int dotCount)
    {
        Vector3 playerPos = GameDelegatesContainer.FuncPlayerPos();
        Ray ray = new Ray(transform.position, (playerPos - transform.position).normalized);
        if (Physics.Raycast(
                ray, out RaycastHit hitInfo, 
                1000, LayersContainer.SIDES_LAYER_MASK, 
                QueryTriggerInteraction.Collide)
            )
        {
            if (hitInfo.collider.TryGetComponent(out DiceSide diceSide))
            {
                diceSide.DotCount = dotCount;
            }
            else
            {
                Debug.LogError("No diceSide in raycast");
            }
        }
        else
        {
            Debug.LogError("Raycast was empty!");
        }
        // _diceSides[(int)_currentSide].DotCount = dotCount;
    }

    private Vector3 GetCurrentSidePos()
    {
        Vector3 playerPos = GameDelegatesContainer.FuncPlayerPos();
        Ray ray = new Ray(transform.position, (playerPos - transform.position).normalized);
        if (Physics.Raycast(
                ray, out RaycastHit hitInfo, 
                1000, LayersContainer.SIDES_LAYER_MASK, 
                QueryTriggerInteraction.Collide)
            )
        {
            if (hitInfo.collider.TryGetComponent(out DiceSide diceSide))
            {
                return diceSide.transform.position;
            }
            else
            {
                Debug.LogError("No diceSide in raycast");
                return Vector3.zero;
            }
        }
        else
        {
            Debug.LogError("Raycast was empty!");
            return Vector3.zero;
        }
    }

    private int GetCurrentSideDotCount()
    {
        Vector3 playerPos = GameDelegatesContainer.FuncPlayerPos();
        Ray ray = new Ray(transform.position, (playerPos - transform.position).normalized);
        if (Physics.Raycast(
                ray, out RaycastHit hitInfo, 
                1000, LayersContainer.SIDES_LAYER_MASK, 
                QueryTriggerInteraction.Collide)
            )
        {
            if (hitInfo.collider.TryGetComponent(out DiceSide diceSide))
            {
                return diceSide.DotCount;
            }
            else
            {
                Debug.LogError("No diceSide in raycast");
                return -1;
            }
        }
        else
        {
            Debug.LogError("Raycast was empty!");
            return -1;
        }
    }

    // private void OnCubeArrivedAtDestination()
    // { 
    //     Ray ray = new Ray(transform.position, (playerPos - transform.position).normalized);
    //     if (Physics.Raycast(
    //             ray, out RaycastHit hitInfo, 
    //             1000, LayersContainer.SIDES_LAYER_MASK, 
    //             QueryTriggerInteraction.Collide)
    //         )
    //     {
    //         if (hitInfo.collider.TryGetComponent(out DiceSide diceSide))
    //         {
    //             return diceSide.DotCount;
    //         }
    //         else
    //         {
    //             Debug.LogError("No diceSide in raycast");
    //             return -1;
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogError("Raycast was empty!");
    //         return -1;
    //     }
    // }
}