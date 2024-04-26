using System.Collections;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField, Min(0)] private float _minValue;
    [SerializeField, Min(0)] private float _maxValue;

    private Coroutine _coroutine;

    public event System.Action Died;

    private void OnValidate()
    {
        if (_minValue >= _maxValue)
            _minValue = _maxValue - 1;
    }

    public void StartDieTimer()
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

        yield return null;

        Died?.Invoke();
        _coroutine = null;
    }
}
