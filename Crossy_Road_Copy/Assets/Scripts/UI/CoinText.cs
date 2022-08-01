using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    private TextMeshProUGUI _ui;
    private void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();
        UpdateText(PlayerPrefs.GetInt("Coin", 0));
    }

    public void UpdateText(int coin) => _ui.text = $"{coin}";

    void OnEnable()
    {
        GameManager.Instance.OnCoinChanged.AddListener(UpdateText);
    }

    void OnDisable()
    {
        GameManager.Instance.OnCoinChanged.RemoveListener(UpdateText);
    }
}
