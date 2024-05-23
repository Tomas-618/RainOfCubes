using UnityEngine;
using AYellowpaper;

public class ObjectsActivityDisplay : BasicObjectsDisplay
{
    [SerializeField] private InterfaceReference<IReadOnlySpawnerEvents, MonoBehaviour> _spawnerEvents;

    private void OnEnable()
    {
        _spawnerEvents.Value.Spawned += IncreaseValueByOne;
        _spawnerEvents.Value.Dispawned += DecreaseValueByOne;
    }

    private void OnDisable()
    {
        if (_spawnerEvents.Value == null)
            return;

        _spawnerEvents.Value.Spawned -= IncreaseValueByOne;
        _spawnerEvents.Value.Dispawned -= DecreaseValueByOne;
    }
}
