using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour, ISpawner
{
    public GameObject CoinPrefab;
    public float TileSize = 60f;

    private int _count;
    private float _position;
    private void Awake()
    {
        _count = (int)(1.5f * Random.value);
        _position = Random.value * TileSize;
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (_count != 0)
        {
            GameObject coin = Instantiate(CoinPrefab, transform.position + _position * transform.forward, transform.rotation);
            //Debug.Log("Coin");
            coin.transform.SetParent(transform);
            --_count;
            // Debug.Log(_position);
            yield return _position = Random.value * TileSize;
        }
    }
}
