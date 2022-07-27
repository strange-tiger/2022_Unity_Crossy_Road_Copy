using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float MoveDistance = 1.5f;
    public float Speed = 100f;
    public float JumpHeight = 1f;

    public Log log { get; private set; }
    public const float LogSpeed = 10f;
    public int coin { get; private set; }

    private Transform _logCompareTransform;
    private PlayerInput _input;
    private Rigidbody _rigidboby;
    private Vector3 _newPosition = new Vector3(0f, 0.5f, 0f);
    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _rigidboby = GetComponent<Rigidbody>();
        coin = PlayerPrefs.GetInt("Coin", 0);
    }

    private bool _onLog = false;
    private int _nextMoveIndex = 0;
    private void FixedUpdate()
    {
        if (_onLog)
        {
            _newPosition += new Vector3(LogSpeed * Time.fixedDeltaTime, 0.5f, 0f);
            _rigidboby.MovePosition(_newPosition);
        }

        if (_nextMoveIndex > _bezierPositions.Length)
        {
            return;
        }
        MoveBezierCurve();
    }

    private void Update()
    {
        float VerticalMovement = MoveDistance * _input.VerticalMove;
        float HorizontalMovement = MoveDistance * _input.HorizontalMove;
        if (VerticalMovement != 0f || HorizontalMovement != 0f)
        {
            _newPosition.x += HorizontalMovement;
            _newPosition.z += VerticalMovement;

            transform.LookAt(transform.position + new Vector3(HorizontalMovement, 0, VerticalMovement));
            SetPositionBezierCurve();
        }
        // _rigidboby.MovePosition(_newPosition);
    }

    private Vector3 _startPosition;
    private Vector3 _middlePosition;
    private Vector3 _endPosition;
    private Vector3[] _bezierPositions;
    private void SetPositionBezierCurve()
    {
        _nextMoveIndex = 0;
        _startPosition = transform.position;
        _endPosition = _newPosition;
        _middlePosition = (_startPosition + _endPosition) / 2 + JumpHeight * Vector3.up;

        _bezierPositions = new Vector3[(int)(1 / (Time.fixedDeltaTime * Speed)) + 1];
        float t = 0f;
        for (int i = 0; i < _bezierPositions.Length; ++i, t += Time.fixedDeltaTime)
        {
            float u = (1 - t);
            Vector3 v1 = u * u * _startPosition;
            Vector3 v2 = 2 * u * t * _middlePosition;
            Vector3 v3 = t * t * _endPosition;

            _bezierPositions[i] = v1 + v2 + v3;
        }
    }

    private void MoveBezierCurve()
    {
        _rigidboby.MovePosition(_bezierPositions[_nextMoveIndex]);
        ++_nextMoveIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Log")
        {
            // Debug.Log(other.name);
            _onLog = true;
        }

        if (other.tag == "Coin")
        {
            ++coin;
            // other.gameObject.SetActive(false);
            Destroy(other.gameObject);
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
