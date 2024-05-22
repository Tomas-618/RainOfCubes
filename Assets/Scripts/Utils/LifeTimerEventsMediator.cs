using UnityEngine;
using AYellowpaper;

public class LifeTimerEventsMediator : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyLifeTimerEvents, MonoBehaviour> _entity;

    public IReadOnlyLifeTimerEvents Entity => _entity.Value;
}
