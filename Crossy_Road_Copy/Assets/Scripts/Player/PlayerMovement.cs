using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 1f;

    public Log log { get; private set; }

    private Transform _logCompareTransform;
    private PlayerInput _input;
    private Vector3 _logOffsetPosition;
    private Vector3 _newPosition = new Vector3(0f, 0.5f, 0f);
    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        float VerticalVector = Speed * _input.VerticalMove;
        float HorizontalVector = Speed * _input.HorizontalMove;

        if (log != null)
        {
            _newPosition = log.transform.position;
        }
        
        _newPosition.x += HorizontalVector;
        _newPosition.z += VerticalVector;
        transform.position = _newPosition;

        if (VerticalVector != 0f || HorizontalVector != 0f)
        {
            transform.LookAt(transform.position + new Vector3(HorizontalVector, 0, VerticalVector));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Log")
        {
            log = other.GetComponent<Log>();

            _logCompareTransform = log.transform;
            _logOffsetPosition = transform.position - log.transform.position;
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Log" && _logCompareTransform == other.transform)
        {
            log = null;
            _logCompareTransform = null;
            _logOffsetPosition = Vector3.zero;
        }
    }
}
