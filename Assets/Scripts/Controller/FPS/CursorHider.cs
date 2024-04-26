using UnityEngine;

public class CursorHider : MonoBehaviour
{
    private void Start() =>
        Cursor.lockState = CursorLockMode.Locked;
}
