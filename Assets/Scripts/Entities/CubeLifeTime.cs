using System.Collections;
using UnityEngine;

public class CubeLifeTime : MonoBehaviour
{
    [SerializeField] private RandomColorChanger _colorChanger;

    [SerializeField, Min(0)] private float _minValue;
    [SerializeField, Min(0)] private float _maxValue;

    private Coroutine _coroutine;
    private bool _isHittedOnPlatform;

    public event System.Action<CubeLifeTime> Died;

    private void OnValidate()
    {
        if (_minValue >= _maxValue)
            _minValue = _maxValue - 1;
    }

    private void OnDisable() =>
        _isHittedOnPlatform = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isHittedOnPlatform)
            return;

        if (collision.transform.GetComponent<Platform>() == false)
            return;

        _isHittedOnPlatform = true;
        _colorChanger.Change();
        StartDieTimer();
    }

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
