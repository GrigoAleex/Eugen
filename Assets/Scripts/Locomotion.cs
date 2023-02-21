using UnityEngine;

internal class Locomotion : MonoBehaviour, ILocomotion
{
    private bool _isGrounded;

    private float _lastOnGroundTime;

    [SerializeField] private float _speed = 14.0f;
    [SerializeField] private float _acceleration = 8.0f;
    [SerializeField] private float _deceleration = 24.0f;
    [SerializeField] private float _velocityPower = 0.87f;

    [SerializeField] private float _friction = 0.25f;

    [SerializeField] private float _jumpForce = 9.0f;
    [SerializeField] private float _jumpBufferTime = 0.1f;
    [SerializeField] private float _fallGravityMultiplier = 2f;

    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    [SerializeField] private bool _canMove = true;

    private void Update()
    {
        _isGrounded = this.CheckForGround();
        if (_isGrounded) _lastOnGroundTime = _jumpBufferTime;

        _lastOnGroundTime -= Time.deltaTime;
    }

    public void UpdateGravity(Rigidbody2D _rigidbody)
    {
        _rigidbody.gravityScale = _rigidbody.velocity.y < 0 ? _fallGravityMultiplier : 1f;
    }

    public void Jump(Rigidbody2D _rigidbody)
    {
        if (_lastOnGroundTime <= 0) return;

        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _rigidbody.AddForce(Vector2.down * _rigidbody.velocity.y / 2, ForceMode2D.Impulse);
    }

    public bool CheckForGround()
    {
        return Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer);
    }

    public void Move(float _input, Rigidbody2D _rigidbody)
    {
        if (!_canMove) return;

        _rigidbody.AddForce(this.GetMovement(_input, _rigidbody) * Vector2.right);

        if (Mathf.Abs(_input) > 0.01f || !_isGrounded) return;

        this.StopMotion(_input, _rigidbody);
    }

    private float GetMovement(float _input, Rigidbody2D _rigidbody)
    {
        float targetSpeed = _input * _speed;
        float speedDifference = targetSpeed - _rigidbody.velocity.x;
        float accelerationRate = Mathf.Abs(targetSpeed) > 0.01f ? _acceleration : _deceleration;

        return Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, _velocityPower) * Mathf.Sign(speedDifference);
    }

    private void StopMotion(float _input, Rigidbody2D _rigidbody)
    {
        float amount = Mathf.Min(Mathf.Abs(_rigidbody.velocity.x), _friction);

        amount *= Mathf.Sign(_rigidbody.velocity.x);

        _rigidbody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
    }

    public void Disable() { _canMove = false; }
    public void Enable() { _canMove = true; }
}
