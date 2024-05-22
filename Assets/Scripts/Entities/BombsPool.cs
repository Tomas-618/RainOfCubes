using UnityEngine;
using Pool;

public class BombsPool : MonoBehaviour, ICanOnlyPutOutInPosition
{
    [SerializeField, Min(0)] private int _maxCount;

    [SerializeField] private Fabric<Bomb> _fabric;

    private ObjectsPool<Bomb> _pool;

    private void Awake() =>
        _pool = new ObjectsPool<Bomb>(_fabric.Create, _maxCount);

    private void OnEnable()
    {
        foreach (Bomb entity in _pool.AllEntities)
            entity.Exploded += _pool.PutInEntity;

        _pool.PutIn += entity => entity.gameObject.SetActive(false);
        _pool.PutOut += entity => entity.gameObject.SetActive(true);
        _pool.Removed += entity => Destroy(entity.gameObject);
    }

    private void OnDisable()
    {
        foreach (Bomb entity in _pool.AllEntities)
            entity.Exploded -= _pool.PutInEntity;

        _pool.PutIn -= entity => entity.gameObject.SetActive(false);
        _pool.PutOut -= entity => entity.gameObject.SetActive(true);
        _pool.Removed -= entity => Destroy(entity.gameObject);
    }

    public void PutOutInPosition(in Vector3 point)
    {
        Bomb bomb = _pool.PutOutEntity();

        if (bomb == null)
            return;

        bomb.transform.position = point;
    }
}
