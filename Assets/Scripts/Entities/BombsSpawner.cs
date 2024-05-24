using System;
using UnityEngine;
using Pool;

public class BombsSpawner : MonoBehaviour, ICanOnlyPutOutInPosition, IReadOnlyBombsSpawnerEvents
{
    [SerializeField, Min(0)] private int _maxCount;

    [SerializeField] private Fabric<Bomb> _fabric;

    private ObjectsPool<Bomb> _pool;

    public event Action Spawned;

    public event Action Dispawned;

    private void Awake() =>
        _pool = new ObjectsPool<Bomb>(_fabric.Create, _maxCount);

    private void OnEnable()
    {
        foreach (Bomb entity in _pool.AllEntities)
            entity.Exploded += _pool.PutInEntity;

        _pool.PutIn += PutInPool;
        _pool.PutOut += PutOutFromPool;
        _pool.Removed += RemoveFromPool;
    }

    private void OnDisable()
    {
        _pool.PutInAllUnstoredEntities();

        foreach (Bomb entity in _pool.AllEntities)
            entity.Exploded -= _pool.PutInEntity;

        _pool.PutIn -= PutInPool;
        _pool.PutOut -= PutOutFromPool;
        _pool.Removed -= RemoveFromPool;
    }

    public void PutOutInPosition(in Vector3 point)
    {
        Bomb bomb = _pool.PutOutEntity();

        if (bomb == false)
            return;

        bomb.transform.position = point;
    }

    private void PutInPool(Bomb entity)
    {
        entity.gameObject.SetActive(false);
        Dispawned?.Invoke();
    }

    private void PutOutFromPool(Bomb entity)
    {
        entity.gameObject.SetActive(true);
        Spawned?.Invoke();
    }

    private void RemoveFromPool(Bomb entity) =>
        Destroy(entity.gameObject);
}
