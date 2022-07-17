using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DiceSymbol : MonoBehaviour
{
    [SerializeField]
    private int _dotCount;

    public int DotCount {get {return _dotCount;}}
}