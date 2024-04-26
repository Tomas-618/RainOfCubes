using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool _isHitted;

    public event Action Hitted;

    private void OnDisable() =>
        _isHitted = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isHitted)
            return;

        if (collision.transform.GetComponent<Platform>() == false)
            return;

        _isHitted = true;
        Hitted?.Invoke();
    }
}
