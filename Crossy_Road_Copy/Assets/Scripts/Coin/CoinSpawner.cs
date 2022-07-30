using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour, ISpawner
{
    public GameObject CoinPrefab;
    public float TileSize = 60f;

    private int _count;
    private float _weight = 1.5f;
    private float _position;
    private void Awake()
    {
        _count = (int)(_weight * Random.value);
        _position = Random.value * TileSize;
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (_count != 0)
        {
            GameObject coin = Instantiate(CoinPrefab, transform.position + _position * transform.forward, transform.rotation);
            coin.transform.SetParent(transform);

            --_count;
            yield return _position = Random.value * TileSize;
        }
    }
}
