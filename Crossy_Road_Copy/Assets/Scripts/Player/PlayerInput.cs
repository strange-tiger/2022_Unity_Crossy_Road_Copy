using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float VerticalMove { get; private set; }
    public float HorizontalMove { get; private set; }

    private void Update()
    {
        // ¿Ãµø≈∞
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