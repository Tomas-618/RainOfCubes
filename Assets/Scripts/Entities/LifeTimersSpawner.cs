using System.Collections;
using UnityEngine;
using Pool;

public class LifeTimersSpawner : MonoBehaviour, IReadOnlyLifeTimersSpawnerEvents
{
    [SerializeField, Min(0)] private int _maxCount;
    [SerializeField, Min(0)] private float _delay;

    [SerializeField] private Fabric<LifeTimer> _fabric;
    [SerializeField] private float _minPosition;
    [SerializeField] private float _maxPosition;
    [SerializeField] private float _spawnHeight;

    private ObjectsPool<LifeTimer> _pool;
    private Transform _transform;
    private Coroutine _coroutine;

    public event System.Action Spawned;

    public event System.Action Dispawned;

    private void Reset() =>
        _maxCount = 4;

    private void OnValidate()
    {
        if (_minPosition >= _maxPosition)
            _minPosition = _maxPosition - 1;
    }

    private void Awake()
    {
        _transform = transform;
        _pool = new ObjectsPool<LifeTimer>(_fabric.Create, _maxCount);
    }

    private void OnEnable()
    {
        foreach (IReadOnlyLifeTimerEvents entity in _pool.AllEntities)
            entity.Died += _pool.PutInEntity;

        _pool.PutIn += PutInPool;
        _pool.PutOut += PutOutFromPool;
        _pool.Removed += RemoveInPool;

        _coroutine = StartCoroutine(SpawnInRandomRange(_delay));
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        foreach (IReadOnlyLifeTimerEvents entity in _pool.AllEntities)
            entity.Died -= _pool.PutInEntity;

        _pool.PutIn -= PutInPool;
        _pool.PutOut -= PutOutFromPool;
        _pool.Removed -= RemoveInPool;
    }

    private void PutInPool(LifeTimer entity)
    {
        entity.gameObject.SetActive(false);
        Dispawned?.Invoke();
    }

    private void PutOutFromPool(LifeTimer entity)
    {
        Vector3 spawnPosition = GetRandomSpawnPosition(_minPosition, _maxPosition, _spawnHeight);

        entity.transform.position = spawnPosition;
        entity.gameObject.SetActive(true);
        Spawned?.Invoke();
    }

    private void RemoveInPool(LifeTimer entity) =>
        Destroy(entity.gameObject);

    private Vector3 GetRandomSpawnPosition(in float minPosition, in float maxPosition, in float spawnHeight)
    {
        return _transform.position + new Vector3(Random.Range(minPosition, maxPosition),
            spawnHeight,
            Random.Range(minPosition, maxPosition));
    }

    private IEnumerator SpawnInRandomRange(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (enabled)
        {
            yield return wait;
            
            _pool.PutOutEntity();
        }
    }
}
