using UnityEngine;

public class BackgroundRotator : MonoBehaviour
{
    [SerializeField]
    private float _amplitude;

    private Vector3 _rotationEuler;
    private void Update()
    {
        _rotationEuler.x = _amplitude * 2* Time.deltaTime;
        _rotationEuler.y = _amplitude * Time.deltaTime;
        _rotationEuler.z = _amplitude * Time.deltaTime;
        transform.Rotate(_rotationEuler, Space.World);
    }
}
