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
            GameObject waterLily = Instantiate(CoinPrefab, transform.position + new Vector3(_position, 0f, 0f), transform.rotation);
            //Debug.Log("Coin");
            waterLily.transform.SetParent(transform);
            --_count;
            // Debug.Log(_position);
            yield return _position = Random.value * TileSize;
        }
    }
}
