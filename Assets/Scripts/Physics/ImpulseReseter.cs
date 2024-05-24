using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ImpulseReseter : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody>();

    private void OnDisable() =>
        _rigidbody.velocity = Vector3.zero;
}
