using UnityEngine;

public class LightsController : MonoBehaviour
{
    [SerializeField]
    private float _speedDeg;

    private Transform _primary;
    private Transform _secondary;

    private void Awake()
    {
        _primary = transform.GetChild(0);
        _secondary = transform.GetChild(1);
    }

    private void Update()
    {
        float angleDelta = _speedDeg * Time.deltaTime;
        _primary.RotateAround(Vector3.zero, Vector3.up, angleDelta);
        _secondary.RotateAround(Vector3.zero, Vector3.up, angleDelta);
    }
}
