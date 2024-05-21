using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class AlphaColorChanger : MonoBehaviour, IReadOnlyAlphaColorChangerEvents
{
    [SerializeField, Range(0, 1)] private float _deltaChanging;

    private MeshRenderer _meshRenderer;
    private Coroutine _coroutine;
    private Color _initial;

    public event Action SetToZero;

    private void Reset() =>
        _deltaChanging = 0.01f;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _initial = _meshRenderer.material.color;
    }

    private void OnEnable() =>
        ChangeToZero();

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _meshRenderer.material.color = _initial;
    }

    private void ChangeToZero()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ProcessChangingColor(Color.clear));
    }

    private IEnumerator ProcessChangingColor(Color desired)
    {
        Color initial = _meshRenderer.material.color;

        float currentDeltaChanging = 0;

        while (_meshRenderer.material.color != desired)
        {
            currentDeltaChanging += _deltaChanging;
            _meshRenderer.material.color = Color.Lerp(initial, desired, currentDeltaChanging);

            yield return null;
        }

        SetToZero?.Invoke();
    }
}
