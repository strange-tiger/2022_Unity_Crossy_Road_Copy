using UnityEngine;

public class MoveTitle : MonoBehaviour
{
    private Vector3 _lastPosition;
    private Vector3 _velocity = Vector3.zero;
    private float _smoothTime = 0.5f;
    private void Awake()
    {
        _lastPosition = transform.position;
        transform.position += new Vector3(-100f, 100f, 0f);
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _lastPosition, ref _velocity, _smoothTime);
    }
}
