using TMPro;
using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(TMP_Text))]
public class UITest : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyLifeTimerSpawnerEvents, MonoBehaviour> _lifeTimerSpawnerEvents;
    [SerializeField] private InterfaceReference<IReadOnlyBombsSpawnerEvents, MonoBehaviour> _bombsSpawnerEvents;

    private TMP_Text _view;

    private void Awake() =>
        _view = GetComponent<TMP_Text>();

    private void OnEnable()
    {
        _lifeTimerSpawnerEvents.Value.Spawned += OnSpawn;
        _lifeTimerSpawnerEvents.Value.Dispawned += OnDispawn;
        _bombsSpawnerEvents.Value.Spawned += OnSpawn;
        _bombsSpawnerEvents.Value.Dispawned += OnDispawn;
    }

    private void OnDisable()
    {
        if (_lifeTimerSpawnerEvents.Value == null || _bombsSpawnerEvents.Value == null)
            return;

        _lifeTimerSpawnerEvents.Value.Spawned -= OnSpawn;
        _lifeTimerSpawnerEvents.Value.Dispawned -= OnDispawn;
        _bombsSpawnerEvents.Value.Spawned -= OnSpawn;
        _bombsSpawnerEvents.Value.Dispawned -= OnDispawn;
    }

    private void OnSpawn()
    {
        int value = Mathf.Clamp(int.Parse(_view.text) + 1, 0, int.MaxValue);

        _view.text = value.ToString();
    }

    private void OnDispawn()
    {
        int value = Mathf.Clamp(int.Parse(_view.text) - 1, 0, int.MaxValue);

        _view.text = value.ToString();
    }
}
