using System.Collections;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField, Min(0)] private float _minValue;
    [SerializeField, Min(0)] private float _maxValue;

    [SerializeField] private Interactable _cube;

    private Coroutine _coroutine;

    public event System.Action<LifeTime> Died;

    private void OnValidate()
    {
        if (_minValue >= _maxValue)
            _minValue = _maxValue - 1;
    }

    private void OnEnable() =>
        _cube.Hitted += StartDieTimer;

    private void OnDisable() =>
        _cube.Hitted -= StartDieTimer;

    private void StartDieTimer()
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(Die(SetRandomValue()));
    }

    private float SetRandomValue() =>
        Random.Range(_minValue, _maxValue);

    private IEnumerator Die(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        yield return wait;

        Died?.Invoke(this);
        _coroutine = null;
    }
}
