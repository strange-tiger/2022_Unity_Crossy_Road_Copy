using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    public bool isOn { get; set; }
    public int coin { get; private set; }

    private TextMeshProUGUI _ui;
    private void Awake()
    {
        _ui = GetComponent<TextMeshProUGUI>();
        coin = PlayerPrefs.GetInt("Coin", 0);
        isOn = true;
    }

    private void Update()
    {
        if (isOn)
        {
            _ui.text = $"{coin}";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coin")
        {
            ++coin;
            // other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
