using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class RandomColorChanger : MonoBehaviour
{
    [SerializeField] private CollisionChecker<Platform> _collisionChecker;

    private MeshRenderer _meshRenderer;

    private void Awake() =>
        _meshRenderer = GetComponent<MeshRenderer>();

    private void OnEnable() =>
        _collisionChecker.Hitted += Change;

    private void OnDisable()
    {
        _collisionChecker.Hitted -= Change;
        _meshRenderer.material.color = Color.white;
    }

    private void Change() =>
        _meshRenderer.material.color = Random.ColorHSV();
}
