using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float Speed = 10f;
    public float MaintenanceTime;

    void Start()
    {
        MaintenanceTime = 50f / Speed;
        Destroy(gameObject, MaintenanceTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(0f, 0f, Speed * Time.fixedDeltaTime);
    }
}