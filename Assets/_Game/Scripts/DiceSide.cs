using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DiceSide : MonoBehaviour
{ 
    public Vector3 Position { get { return transform.position; } }
    public Vector3 InsidePosition { get { return transform.position - transform.forward * 0.2f; } }
    public Vector3 OutsidePosition { get { return transform.position + transform.forward * 0.2f; } }
    public int DotCount { get; set; }
}