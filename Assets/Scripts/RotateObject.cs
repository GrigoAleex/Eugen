using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private Transform _transform;
    public float _speed = 5.0f;
    public int _direction = -1;

    void Update()
    {
        _transform.Rotate(0, 0, _speed * Time.deltaTime * _direction, Space.World);
    }
}
