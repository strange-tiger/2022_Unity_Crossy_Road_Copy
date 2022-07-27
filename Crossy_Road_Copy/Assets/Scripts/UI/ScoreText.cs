using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public int score { get; private set; }

    private TextMeshProUGUI _ui;
    private void Awake()
    {
        // PlayerPrefs.DeleteAll();
        _ui = GetComponent<TextMeshProUGUI>();    
        score = 0;
    }

    private void Update()
    {
        float currentPosition = playerMovement.transform.position.z / playerMovement.Speed;
          if(score + 1 <= currentPosition)
          {
            score = (int)currentPosition;
          }
          _ui.text = $"{score}";
    }
}
