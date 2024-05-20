using System;
using UnityEngine;

public abstract class CollisionChecker<T> : MonoBehaviour where T : MonoBehaviour
{
    private bool _isHitted;

    public event Action Hitted;

    private void OnDisable() =>
        _isHitted = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isHitted)
            return;

        if (collision.transform.GetComponent<T>() == false)
            return;

        _isHitted = true;
        Hitted?.Invoke();
    }
}
