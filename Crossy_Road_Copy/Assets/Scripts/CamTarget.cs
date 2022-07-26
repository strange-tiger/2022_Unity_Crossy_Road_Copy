using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTarget : MonoBehaviour
{
    public Transform Player;

    private Vector3 _target;
    private void Awake()
    {
        transform.position = Player.position;
    }

    private void FixedUpdate()
    {
        float elapsedTime = Time.fixedDeltaTime;
        Vector3 playerPosition = Player.position;
        Vector3 thisPosition = transform.position;

        if (playerPosition.z > transform.position.z)
        {
            thisPosition.z += (playerPosition.z - thisPosition.z) * elapsedTime;
        }
        else
        {
            thisPosition += new Vector3(0f, 0f, elapsedTime);
        }

        transform.position = thisPosition;
    }
}
