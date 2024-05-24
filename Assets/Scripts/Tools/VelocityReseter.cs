using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VelocityReseter : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody>();

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
