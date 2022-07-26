using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void Die()
    {
        gameObject.SetActive(false);
    }

    private Camera _mainCam;
    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void FixedUpdate()
    {
        Vector3 viewPosition = _mainCam.WorldToViewportPoint(transform.position);

        if (viewPosition.x < 0f || viewPosition.x > 1f || viewPosition.y < 0f || viewPosition.y > 1f)
        {
            Debug.Log($"Camera : {viewPosition}");
            Die();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log($"OnCollision : {collision.gameObject}");
    //    if (collision.gameObject.tag == "Obstacles")
    //    {
    //        Die();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTrigger : {other.gameObject}");
        if (other.tag == "Obstacles")
        {
            Die();
        }
    }
}
