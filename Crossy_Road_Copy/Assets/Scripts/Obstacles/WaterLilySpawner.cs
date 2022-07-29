using System.Collections;
using UnityEngine;

public class WaterLilySpawner : MonoBehaviour, ISpawner
{
    public GameObject WaterLilyPrefab;
    public int MinWaterLilyNum = 1;
    public int MaxWaterLilyNum = 5;
    public Transform RiverTransform;
    
    private int _count;
    private float _position;
    private int _tileLengthHalf;
    private float _distance;
    private void Awake()
    {
        _count = Random.Range(MinWaterLilyNum, MaxWaterLilyNum);
        _tileLengthHalf = (int)(RiverTransform.localScale.x / (2 * _count));
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (_count != 0)
        {
            _distance += 2 * Random.Range(1, _tileLengthHalf);
            _position += _distance;
            GameObject waterLily = Instantiate(WaterLilyPrefab, gameObject.transform.position + _position * transform.forward, gameObject.transform.rotation);
            waterLily.transform.SetParent(transform);
            
            yield return --_count;
        }
    }
}
