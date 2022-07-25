using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float VerticalMove { get; private set; }
    public float HorizontalMove { get; private set; }

    private void Update()
    {
        // 게임 오버 관련

        // 이동키
        if(Input.GetKeyDown(KeyCode.W))
        {
            VerticalMove = 1f;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            VerticalMove = -1f;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            HorizontalMove = 1f;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            HorizontalMove = -1f;
        }
        else
        {
            VerticalMove = 0f;
            HorizontalMove = 0f;
        }
    }
}
