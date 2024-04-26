using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class RandomColorChanger : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    private void OnDisable() =>
        _meshRenderer.material.color = Color.white;

    private void Start() =>
        _meshRenderer = GetComponent<MeshRenderer>();

    public void Change() =>
        _meshRenderer.material.color = RandomUtils.GetRandomColor();
}
