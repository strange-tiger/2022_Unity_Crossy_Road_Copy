using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public enum Direction
    {
        idle,
        forward,
        back,
        right,
        left
    }

    public float VerticalMove { get; private set; }
    public float HorizontalMove { get; private set; }

    private float _swipeRange = 1f;
    private void Update()
    {
#if UNITY_ANDROID
        GetTouchInput();
#else
        GetKeyInput();
        GetMouseInput();
#endif
        SetMovement();
        direction = Direction.idle;
    }

    private Direction direction = Direction.idle;
    private void SetMovement()
    {
        switch(direction)
        {
            case Direction.forward:  VerticalMove = 1f; break;
            case Direction.back:    VerticalMove = -1f; break;
            case Direction.right:  HorizontalMove = 1f; break;
            case Direction.left:  HorizontalMove = -1f; break;
            default: VerticalMove = 0f; HorizontalMove = 0f; break;
        }
    }

    private void GetKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Direction.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Direction.back;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Direction.right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Direction.left;
        }
        else
        {
            direction = Direction.idle;
        }
    }

    private void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _prevMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            CalculateMouseInput();
        }
    }

    private Vector3 _currentMousePosition;
    private Vector3 _prevMousePosition;
    public void CalculateMouseInput()
    {
        _currentMousePosition = Input.mousePosition;
           
        Vector3 differ = _currentMousePosition - _prevMousePosition;
        if (Mathf.Abs(differ.x) > Mathf.Abs(differ.y))
        {
            if (differ.x > _swipeRange)
            {
                direction = Direction.right;
            }
            else if (differ.x < -_swipeRange)
            {
                direction = Direction.left;
            }
            else
            {
                direction = Direction.forward;
            }
        }
        else
        {
            if (differ.y < -_swipeRange)
            {
                direction = Direction.back;
            }
            else
            {
                direction = Direction.forward;
            }
        }
    }

    private void GetTouchInput()
    {
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);
            CalculateTouchInput(ref touch);
        }
    }

    private Vector2 _currentTouchPosition;
    private Vector2 _prevTouchPosition;
    public void CalculateTouchInput(ref Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            _prevTouchPosition = touch.position;
        }
        if (touch.phase == TouchPhase.Ended)
        {
            _currentTouchPosition = Input.mousePosition;


            Vector3 differ = _currentTouchPosition - _prevTouchPosition;
            if (Mathf.Abs(differ.x) > Mathf.Abs(differ.y))
            {
                if (differ.x > _swipeRange)
                {
                    direction = Direction.right;
                }
                else if (differ.x < -_swipeRange)
                {
                    direction = Direction.left;
                }
                else
                {
                    direction = Direction.forward;
                }
            }
            else
            {
                if (differ.y < -_swipeRange)
                {
                    direction = Direction.back;
                }
                else
                {
                    direction = Direction.forward;
                }
            }
        }
    }
}