using UnityEngine;

public class LightsController : MonoBehaviour
{
    [SerializeField]
    private float _speedDegPrimary;

    [SerializeField]
    private float _speedDegSecondary;

    [SerializeField]
    private float _secondaryOffset;


    private Transform _primary;
    private Transform _secondary;

    private void Awake()
    {
        _primary = transform.GetChild(0);
        _secondary = transform.GetChild(1);

        _secondary.RotateAround(Vector3.zero, Vector3.right, _secondaryOffset);
    }

    private void Update()
    {
        float angleDelta = _speedDegPrimary * Time.deltaTime;
        _primary.RotateAround(Vector3.zero, Vector3.up, angleDelta);
        angleDelta = _speedDegSecondary * Time.deltaTime;
        _secondary.RotateAround(Vector3.zero, Vector3.right, angleDelta);
    }
}
