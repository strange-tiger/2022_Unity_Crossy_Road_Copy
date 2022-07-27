using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTarget : MonoBehaviour
{
    public Transform Player;
    public bool isOn { get; set; }

    private Vector3 _target;
    private void Awake()
    {
        transform.position = Player.position;
        isOn = true;
    }

    private void FixedUpdate()
    {
        if(!isOn)
        {
            return;
        }

        Vector3 playerPosition = Player.position;
        Vector3 thisPosition = transform.position;

        if (playerPosition.z - 1 > transform.position.z)
        {
            thisPosition.z += (playerPosition.z - thisPosition.z) * Time.fixedDeltaTime;
        }
        else
        {
            thisPosition += new Vector3(0f, 0f, Time.fixedDeltaTime);
        }

        transform.position = thisPosition;
    }
}
