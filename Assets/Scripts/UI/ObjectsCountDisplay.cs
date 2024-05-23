using UnityEngine;
using AYellowpaper;

public class ObjectsCountDisplay : BasicObjectsDisplay
{
    [SerializeField] private InterfaceReference<IReadOnlySpawnerEvents, MonoBehaviour> _spawnerEvents;

    private void OnEnable() =>
        _spawnerEvents.Value.Spawned += IncreaseValueByOne;

    private void OnDisable()
    {
        if (_spawnerEvents.Value == null)
            return;

        _spawnerEvents.Value.Spawned -= IncreaseValueByOne;
    }
}
