using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class AlphaColorChanger : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _deltaChanging;

    private MeshRenderer _meshRenderer;

    private void Reset() =>
        _deltaChanging = 0.01f;

    private void Awake() =>
        _meshRenderer = GetComponent<MeshRenderer>();

    private void OnEnable()
    {
        ChangeToZero();
    }

    private void OnDisable()
    {
        
    }

    private void ChangeToZero()
    {
        StartCoroutine(ProcessChangingColor(Color.clear));
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
    }
}
