using System.Collections;
using UnityEngine;
using Pool;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField, Min(0)] private int _maxCount;
    [SerializeField, Min(0)] private float _delay;

    [SerializeField] private CubeLifeTime _entity;
    [SerializeField] private float _minPosition;
    [SerializeField] private float _maxPosition;
    [SerializeField] private float _spawnHeight;

    private Transform _transform;
    private ObjectsPool<CubeLifeTime> _pool;

    private void Reset() =>
        _maxCount = 4;

    private void Awake() =>
        _pool = new ObjectsPool<CubeLifeTime>(() => Instantiate(_entity), _maxCount);

    private void OnEnable()
    {
        foreach (CubeLifeTime cube in _pool.Entities)
            cube.Died += _pool.PutInEntity;

        _pool.PutIn += cube => cube.gameObject.SetActive(false);
        _pool.PutOut += GetFromPool;
    }

    private void OnDisable()
    {
        foreach (CubeLifeTime cube in _pool.Entities)
            cube.Died -= _pool.PutInEntity;

        _pool.PutIn -= cube => cube.gameObject.SetActive(false);
        _pool.PutOut -= GetFromPool;
    }

    private void Start()
    {
        _transform = transform;

        StartCoroutine(SpawnInRandomRange(_delay));
    }

    private void GetFromPool(CubeLifeTime cube)
    {

        Vector3 spawnPosition = GetRandomSpawnPosition(_minPosition, _maxPosition, _spawnHeight);

        cube.transform.position = spawnPosition;
        cube.gameObject.SetActive(true);
    }

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
