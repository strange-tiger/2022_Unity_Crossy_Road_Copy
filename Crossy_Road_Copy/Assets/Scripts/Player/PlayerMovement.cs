using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float MoveDistance = 1.0f;
    public float JumpHeight = 1f;
    public float Speed = 5f;

    public float LogSpeed = 5f;
   
    private Transform _logCompareTransform;
    private PlayerInput _input;
    private Rigidbody _rigidboby;
    private Vector3 _newPosition = new Vector3(0f, 0.5f, 0f);
    private float _speed;
    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _rigidboby = GetComponent<Rigidbody>();
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
            }
        }
        
        if (_onMove)
        {
            MoveBezierCurve();
        }
        
        if (_onTree)
        {
            StopMoveBezierCurve();
            _newPosition = _prevPosition;
            _rigidboby.MovePosition(_prevPosition);
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

        if(!_onMove && !_onLog)
        {
            AdjustPosition();
        }
    }

    private void RecordPrevPosition()
    {
        _prevPosition = transform.position;
    }

    private void AdjustPosition()
    {
        Vector3 _adjustment = transform.position;
        _adjustment.x = Mathf.Round(_adjustment.x);
        _adjustment.z = Mathf.Round(_adjustment.z);
        transform.position = _adjustment;
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

        if (_bezierTime > 1f)
        {
            _rigidboby.MovePosition(_endPosition);
            StopMoveBezierCurve();
        }
        else
        {
            DrawBezierCurve();
            _rigidboby.MovePosition(_bezierPositions);
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
        _onFloor = true;
        RecordPrevPosition();
    }

    private void OnCollisionExit(Collision collision)
    {
        _onFloor = false;
        _onFloorTime = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tree")
        {
            _onTree = true;
        }

        if (other.tag == "Log")
        {
            _onLog = true;

            _logDirection = other.transform.forward;
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
