using UnityEngine;
using AYellowpaper;
using Pool;

public class BombsSpawner : MonoBehaviour
{
    [SerializeField, Min(0)] private int _maxCount;

    [SerializeField] private InterfaceReference<IReadOnlyLifeTimerSpawner, MonoBehaviour> _lifeTimersSpawner;
    [SerializeField] private Fabric<Bomb> _fabric;

    private ObjectsPool<Bomb> _pool;

    private void Awake() =>
        _pool = new ObjectsPool<Bomb>(_fabric.Create, _maxCount);

    private void OnEnable()
    {
        foreach (IReadOnlyLifeTimerEvents lifeTimerEvents in _lifeTimersSpawner.Value.AllEntitiesEvents)
            lifeTimerEvents.Died += GetFromPool;

        foreach (Bomb entity in _pool.AllEntities)
            entity.Exploded += _pool.PutInEntity;

        _pool.PutIn += entity => entity.gameObject.SetActive(false);
        _pool.PutOut += entity => entity.gameObject.SetActive(true);
        _pool.Removed += entity => Destroy(entity.gameObject);
    }

    private void OnDisable()
    {
        foreach (IReadOnlyLifeTimerEvents lifeTimerEvents in _lifeTimersSpawner.Value.AllEntitiesEvents)
            lifeTimerEvents.Died -= GetFromPool;

        foreach (Bomb entity in _pool.AllEntities)
            entity.Exploded -= _pool.PutInEntity;

        _pool.PutIn -= entity => entity.gameObject.SetActive(false);
        _pool.PutOut -= entity => entity.gameObject.SetActive(true);
        _pool.Removed -= entity => Destroy(entity.gameObject);
    }

    private void GetFromPool(LifeTimer timer)
    {
        Bomb bomb = _pool.PutOutEntity();

        if (bomb == null)
            return;

        bomb.transform.position = timer.transform.position;
    }
}
