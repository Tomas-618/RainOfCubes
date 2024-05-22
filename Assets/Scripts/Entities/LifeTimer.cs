using System.Collections;
using UnityEngine;
using AYellowpaper;

public class LifeTimer : MonoBehaviour, IReadOnlyLifeTimerEvents, IInitializable<ICanOnlyPutOutInPosition>
{
    [SerializeField, Min(0)] private float _minDuration;
    [SerializeField, Min(0)] private float _maxDuration;

    [SerializeField] private InterfaceReference<ICanOnlyInitializeLifeTimerDieEventsHandler, MonoBehaviour> _dieEventHandler;
    [SerializeField] private CollisionChecker<Platform> _collisionChecker;

    private Coroutine _coroutine;

    public event System.Action<LifeTimer> Died;

    private void OnValidate()
    {
        if (_minDuration >= _maxDuration)
            _minDuration = _maxDuration - 1;
    }

    private void OnEnable() =>
        _collisionChecker.Hitted += StartCountdown;

    private void OnDisable() =>
        _collisionChecker.Hitted -= StartCountdown;

    public void Init(ICanOnlyPutOutInPosition value) =>
        _dieEventHandler.Value.Init(value);

    private void StartCountdown()
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(Die(GetRandomValue()));
    }

    private float GetRandomValue() =>
        Random.Range(_minDuration, _maxDuration);

    private IEnumerator Die(float delay)
    {
        WaitForSeconds wait = new WaitForSeconds(delay);

        yield return wait;

        Died?.Invoke(this);
        _coroutine = null;
    }
}
