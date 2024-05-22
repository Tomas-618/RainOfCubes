using System;
using UnityEngine;

public class LifeTimerDieEventHandler : MonoBehaviour, ICanOnlyInitializeLifeTimerDieEventsHandler
{
    [SerializeField] private LifeTimerEventsMediator _mediator;

    private ICanOnlyPutOutInPosition _bombsPool;

    private void OnEnable()
    {
        if (_bombsPool == null)
            return;

        _mediator.Entity.Died += PutOutBombInEntity;
    }

    private void OnDisable()
    {
        if (_bombsPool == null)
            return;

        _mediator.Entity.Died -= PutOutBombInEntity;
    }

    public void Init(ICanOnlyPutOutInPosition bombsPool) =>
        _bombsPool = bombsPool ?? throw new ArgumentNullException(nameof(bombsPool));

    private void PutOutBombInEntity(LifeTimer entity) =>
        _bombsPool.PutOutInPosition(entity.transform.position);
}
