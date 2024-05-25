using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public abstract class BasicObjectsDisplay : MonoBehaviour
{
    private TMP_Text _view;

    private void Awake() =>
        _view = GetComponent<TMP_Text>();

    protected void IncreaseValueByOne()
    {
        int value = Mathf.Clamp(int.Parse(_view.text) + 1, 0, int.MaxValue);

        SetValue(value);
    }

    protected void DecreaseValueByOne()
    {
        int value = Mathf.Clamp(int.Parse(_view.text) - 1, 0, int.MaxValue);

        SetValue(value);
    }

    private void SetValue(in int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(value.ToString());

        _view.text = value.ToString();
    }
}
