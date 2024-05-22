using TMPro;
using UnityEngine;
using AYellowpaper;

[RequireComponent(typeof(TMP_Text))]
public class SpawnerTextUI : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlySpawnerEvents, MonoBehaviour> _events;

    private TMP_Text _view;

    private void Awake() =>
        _view = GetComponent<TMP_Text>();

    private void OnEnable() =>
        _events.Value.Spawned += SetValue;

    private void OnDisable()
    {
        if (_events.Value == null)
            return;

        _events.Value.Spawned -= SetValue;
    }

    private void SetValue()
    {
        int value = Mathf.Clamp(int.Parse(_view.text) + 1, 0, int.MaxValue);

        _view.text = value.ToString();
    }
}
