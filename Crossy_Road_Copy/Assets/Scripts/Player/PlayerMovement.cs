using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 1.5f;

    public Log log { get; private set; }

    private Transform _logCompareTransform;
    private PlayerInput _input;
    private Rigidbody _rigidboby;
    private Vector3 _newPosition = new Vector3(0f, 0.5f, 0f);
    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _rigidboby = GetComponent<Rigidbody>();
    }

    private float _logSpeed = 10f;
    private bool _onLog = false;
    private void FixedUpdate()
    {
        if (_onLog)
        {
            _newPosition += new Vector3(_logSpeed * Time.fixedDeltaTime, 0.5f, 0f);
        }
    }

    private void Update()
    {
        float VerticalMovement = Speed * _input.VerticalMove;
        float HorizontalMovement = Speed * _input.HorizontalMove;
        if (VerticalMovement != 0f || HorizontalMovement != 0f)
        {
            _newPosition.x += HorizontalMovement;
            _newPosition.z += VerticalMovement;

            transform.LookAt(transform.position + new Vector3(HorizontalMovement, 0, VerticalMovement));
        }
        _rigidboby.MovePosition(_newPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Log")
        {
            // Debug.Log(other.name);
            _onLog = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Log")
        {
            _onLog = false;
        }
    }
}
