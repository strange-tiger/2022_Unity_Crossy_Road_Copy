using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    public PlayerMovement player;

    private TextMeshProUGUI _ui;
    private void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
       _ui.text = $"{player.coin}";
    }
}
