using UnityEngine;

public class TransformMover : MonoBehaviour
{
    [SerializeField, Min(0)] private float _walkingSpeed;
    [SerializeField, Min(0)] private float _runningSpeed;

    private Transform _transform;
    private float _speed;

    private void OnValidate()
    {
        if (_walkingSpeed >= _runningSpeed)
            _walkingSpeed = _runningSpeed - 1;
    }

    private void Reset()
    {
        _walkingSpeed = 10;
        _runningSpeed = 20;
    }

    private void Start()
    {
        _speed = _walkingSpeed;
        _transform = transform;
    }

    private void Update() =>
        Move();

    private void Move()
    {
        Vector3 move = new Vector3(Input.GetAxis(AxisNames.Horizontal),
            Input.GetAxis(AxisNames.Jump),
            Input.GetAxis(AxisNames.Vertical));

        _speed = GetSpeed(_walkingSpeed, _runningSpeed);

        _transform.Translate(move * _speed * Time.deltaTime);
    }

    private float GetSpeed(in float walkingSpeed, in float runningSpeed) =>
        Input.GetKey(KeyCode.LeftShift) ? runningSpeed : walkingSpeed;
}
