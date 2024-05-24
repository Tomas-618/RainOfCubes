using UnityEngine;

public class RotationReseter : MonoBehaviour
{
    private Transform _transform;

    private void Awake() =>
        _transform = transform;

    private void OnDisable() =>
        _transform.rotation = Quaternion.identity;
}
