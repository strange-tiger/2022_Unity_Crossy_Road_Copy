using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float VerticalMove { get; private set; }
    public float HorizontalMove { get; private set; }

    //private Animator _animator;
    //private bool _isHopping = false;
    private void Awake()
    {
        //_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // ¿Ãµø≈∞
        if(Input.GetKeyDown(KeyCode.W))
        {
            VerticalMove = 1f;
            //_isHopping = true;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            VerticalMove = -1f;
            //_isHopping = true;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            HorizontalMove = 1f;
            //_isHopping = true;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            HorizontalMove = -1f;
            //_isHopping= true;
        }
        else
        {
            VerticalMove = 0f;
            HorizontalMove = 0f;
            //_isHopping = false;
        }

        //if(_isHopping)
        //{
        //    _animator.SetTrigger("isHopping");
        //}
    }
}
