using System;
using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour
{
    [SerializeField, Min(0)] private float _impulse;
    [SerializeField, Min(0)] private float _radius;

    [SerializeField] private InterfaceReference<IReadOnlyAlphaColorChangerEvents, MonoBehaviour> _alphaColorChangerEvents;

    private Rigidbody _rigidbody;

    public event Action<Bomb> Exploded;

    private void Reset()
    {
        _impulse = 20;
        _radius = 15;
    }

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody>();

    private void OnEnable() =>
        _alphaColorChangerEvents.Value.SetToZero += Explode;

    private void OnDisable() =>
        _alphaColorChangerEvents.Value.SetToZero -= Explode;

    private void Explode()
    {
        Collider[] others = Physics.OverlapSphere(_rigidbody.position, _radius);

        foreach (Collider other in others)
        {
            if (other.TryGetComponent(out Rigidbody rigidbody))
            {
                if (other.GetComponent<Bomb>() == false)
                {
                    rigidbody.AddExplosionForce(_impulse, _rigidbody.position, _radius, 0, ForceMode.Impulse);
                }
            }
        }

        Exploded?.Invoke(this);
    }
}
