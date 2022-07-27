using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI BestScoreUI;

    public void Activate(int bestScore)
    {
        gameObject.SetActive(true);
        BestScoreUI.text = $"{bestScore}√ ";
    }
}
