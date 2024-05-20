using UnityEngine;

public abstract class Fabric<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _entity;
    [SerializeField] private Transform _parent;

    public virtual T Create() =>
        Instantiate(_entity, _parent);
}
