using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float MoveDistance = 1.5f;
    public float JumpHeight = 1f;
    public float Speed = 5f;


    public float LogSpeed = 10f;
    public int coin { get; private set; }

    private Transform _logCompareTransform;
    private PlayerInput _input;
    private Rigidbody _rigidboby;
    private Vector3 _newPosition = new Vector3(0f, 0.5f, 0f);
    private float _speed;
    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _rigidboby = GetComponent<Rigidbody>();
        coin = PlayerPrefs.GetInt("Coin", 0);
        _speed = Speed;
    }

    private Vector3 _logDirection;
    private bool _onLog = false;
    private void FixedUpdate()
    {
        if (_onLog)
        {
            RecordPrevPosition();
            _newPosition += LogSpeed * Time.fixedDeltaTime * _logDirection;
            _rigidboby.MovePosition(_newPosition);
            StopMoveBezierCurve();
        }
    }

    private bool _onMove = false;
    private bool _onTree = false;
    private bool _onFloor = false;
    private float _onFloorTime = 0f;
    private Vector3 _prevPosition;
    private void Update()
    {
        float horizontalMovement = MoveDistance * _input.HorizontalMove;
        float verticalMovement = MoveDistance * _input.VerticalMove;

        if (horizontalMovement != 0f || verticalMovement != 0f)
        {
            if (!_onMove)
            {
                SetPositionBezierCurve(horizontalMovement, verticalMovement);
            }
            else
            {
                _speed *= Speed;
                Debug.Log(_speed);
            }
        }
        
        if (_onMove)
        {
            MoveBezierCurve();
        }

        if (_onTree)
        {
            _newPosition = _prevPosition;
            _rigidboby.MovePosition(_prevPosition);
            StopMoveBezierCurve();
            _onTree = !_onTree;
        }


        if (_onFloor)
        {
            _onFloorTime += Time.deltaTime;
            if(_onFloorTime > 0.5f)
            {
                _speed = Speed;
            }
        }
    }

    private void RecordPrevPosition()
    {
        _prevPosition = transform.position;
    }

    private float _bezierTime = 0f;
    private Vector3 _startPosition;
    private Vector3 _middlePosition;
    private Vector3 _endPosition;
    private Vector3 _bezierPositions;
    private void SetPositionBezierCurve(float horizontalMovement, float verticalMovement)
    {
        _newPosition.x += horizontalMovement;
        _newPosition.z += verticalMovement;

        Vector3 lookDirection = transform.position + horizontalMovement * Vector3.right + verticalMovement * Vector3.forward;
        transform.LookAt(lookDirection);

        _startPosition = transform.position;
        _endPosition = _newPosition;
        _middlePosition = (_startPosition + _endPosition) / 2 + JumpHeight * Vector3.up;
        _onMove = true;
        // _speed = Speed;
    }

    private void DrawBezierCurve()
    {
        Vector3 pointA = Vector3.Lerp(_startPosition, _middlePosition, _bezierTime);
        Vector3 pointB = Vector3.Lerp(_middlePosition, _endPosition, _bezierTime);

        _bezierPositions = Vector3.Lerp(pointA, pointB, _bezierTime);
    }

    private void MoveBezierCurve()
    {
        _bezierTime += _speed * Time.deltaTime;
        //Debug.Log(_bezierTime);

        if (_bezierTime > 1f)
        {
            _rigidboby.MovePosition(_endPosition);
            StopMoveBezierCurve();
        }
        else
        {
            DrawBezierCurve();
            _rigidboby.MovePosition(_bezierPositions);
            // Debug.Log("Move");
        }
    }

    private void StopMoveBezierCurve()
    {
        _onMove = false;
        _bezierTime = 0f;
        
        _startPosition = Vector3.zero;
        _middlePosition = Vector3.zero;
        _endPosition = Vector3.zero;
        _bezierPositions = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("Floor");
        _onFloor = true;
        RecordPrevPosition();
    }

    private void OnCollisionExit(Collision collision)
    {
        // Debug.Log("Floor Exit");
        _onFloor = false;
        _onFloorTime = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tree")
        {
            // Debug.Log("Tree");
            _onTree = true;
        }

        if (other.tag == "Log")
        {
            // Debug.Log(other.name);
            _onLog = true;
            
            _logDirection = other.transform.forward;
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
