using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _input;

    private ILocomotion _locomotion;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _locomotion = GetComponent<ILocomotion>();
    }

    private void Update()
    {
        _input = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _locomotion.Jump(_rigidbody);
        }

        _locomotion.UpdateGravity(_rigidbody);
    }

    private void FixedUpdate()
    {
        _locomotion.Move(_input, _rigidbody);
    }
}
