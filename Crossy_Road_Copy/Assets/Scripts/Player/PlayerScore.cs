using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    void Update()
    {
        GetScore();
    }

    private void GetScore()
    {
        float currentPosition = transform.position.z;
        int score = GameManager.Instance.CurrentScore;
        if (score + 1 <= currentPosition)
        {
            GameManager.Instance.AddScore();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            GameManager.Instance.AddCoin();
            // other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
