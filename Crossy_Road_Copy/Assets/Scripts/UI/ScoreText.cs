using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public bool isOn { get; set; }
    public int score { get; private set; }

    private TextMeshProUGUI _ui;
    private void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();    
        score = 0;
        isOn = true;
    }

    private void Update()
    {
        float currentPosition = playerMovement.transform.position.z / playerMovement.Speed;
        if (isOn)
        {
            if(score + 1 <= currentPosition)
            {
                score = (int)currentPosition;
            }
            _ui.text = $"{score}";
        }
    }
}
