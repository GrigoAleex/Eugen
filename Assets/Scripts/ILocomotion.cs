using UnityEngine;

internal interface ILocomotion
{
    bool CheckForGround();
    void Move(float _input, Rigidbody2D _rigidbody);
    void Jump(Rigidbody2D _rigidbody);
    void UpdateGravity(Rigidbody2D _rigidbody);
}