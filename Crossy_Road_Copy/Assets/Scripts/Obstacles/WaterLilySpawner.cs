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
    private float _distance;
    private void Awake()
    {
        _count = 2 * Random.Range(MinWaterLilyNum, MaxWaterLilyNum);
        _distance = RiverTransform.localScale.x / _count;
        _position = _distance;
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (_count != 0)
        {
            GameObject waterLily = Instantiate(WaterLilyPrefab, gameObject.transform.position + _position * transform.forward, gameObject.transform.rotation);
            waterLily.transform.SetParent(transform);
            
            --_count;
            yield return _position += _distance;
        }
    }
}
