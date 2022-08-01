using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float Speed = 10f;
    //public float MaintenanceTime;
    
    //void Start()
    //{
    //    MaintenanceTime = 120f / Speed;
    //}

    //private float _elapsedTime = 0f;
    private void FixedUpdate()
    {
        transform.Translate(0f, 0f, Speed * Time.fixedDeltaTime);

        //_elapsedTime += Time.fixedDeltaTime;
        //if(_elapsedTime > MaintenanceTime)
        //{
        //    _elapsedTime = 0f;
        //    gameObject.SetActive(false);
        //}
    }
}
