using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int coin { get; private set; }
    public int score { get; private set; }

    private void Awake()
    {
        coin = PlayerPrefs.GetInt("Coin", 0);
        score = 0;
    }

    void Update()
    {
        GetScore();
    }

    private void GetScore()
    {
        float currentPosition = transform.position.z;
        if (score + 1 <= currentPosition)
        {
            score = (int)currentPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            ++coin;
            // other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
