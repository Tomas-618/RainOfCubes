using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class RandomColorChanger : MonoBehaviour
{
    [SerializeField] private Interactable _cube;

    private MeshRenderer _meshRenderer;

    private void Awake() =>
        _meshRenderer = GetComponent<MeshRenderer>();

    private void OnEnable() =>
        _cube.Hitted += Change;

    private void OnDisable()
    {
        _cube.Hitted -= Change;
        _meshRenderer.material.color = Color.white;
    }

    private void Change() =>
        _meshRenderer.material.color = RandomUtils.GetRandomColor();
}
