using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    public PlayerScore player;

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
