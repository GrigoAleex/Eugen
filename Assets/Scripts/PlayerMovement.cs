using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _input;

    private bool _isGrounded;
    private bool _isJumping;
    private bool _isJumpCut;
    private bool _isJumpFalling;

    private float _lastOnGroundTime;
    private float _lastOnJumpTime;

    [SerializeField] private float _speed = 9.0f;
    [SerializeField] private float _acceleration = 13.0f;
    [SerializeField] private float _deceleration = 16.0f;
	[SerializeField] private float _velocityPower = 0.96f;
	
	[SerializeField] private float _friction = 0.22f;

    [SerializeField] private float _jumpForce = 13.0f;
    [SerializeField] private float _jumpBufferTime = 0.3f;
    [SerializeField] private float _fallGravityMultiplier = 2f;

    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);

    private void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _lastOnGroundTime = 0;
        _lastOnJumpTime = _jumpBufferTime;
        _isJumping = true;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
       _input = Input.GetAxisRaw("Horizontal");

        _isGrounded = Physics2D.OverlapBox(
            _groundCheckPoint.position, 
            _groundCheckSize, 
            0, 
            _groundLayer
        );

        if (_isGrounded)
        {
            _lastOnGroundTime = _jumpBufferTime;
        }
           
        if (
            Input.GetKeyDown(KeyCode.Space) && 
            _lastOnGroundTime > 0
        ) Jump();

        if (
            Input.GetKeyUp(KeyCode.Space) &&
            _isJumping &&
            _rigidbody.velocity.y > 0
        ) 
        {
            _rigidbody.AddForce(
                Vector2.down * _rigidbody.velocity.y / 2,
                ForceMode2D.Impulse
            );
            _lastOnJumpTime = 0;
        }

        _rigidbody.gravityScale = _rigidbody.velocity.y < 0 ? _fallGravityMultiplier : 1;

        _lastOnGroundTime -= Time.deltaTime;
        _lastOnJumpTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
		float targetSpeed = _input * _speed;
		float speedDifference = targetSpeed - _rigidbody.velocity.x;
		float accelerationRate = Mathf.Abs(targetSpeed) > 0.01f ? _acceleration : _deceleration; 
		float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, _velocityPower) * Mathf.Sign(speedDifference);
		
		_rigidbody.AddForce(movement * Vector2.right);

		if (Mathf.Abs(_input) < 0.01f && _isGrounded) 
		{
			float amount = Mathf.Min(Mathf.Abs(_rigidbody.velocity.x), _friction);

			amount *= Mathf.Sign(_rigidbody.velocity.x);

			_rigidbody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
		}
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
    }
}
