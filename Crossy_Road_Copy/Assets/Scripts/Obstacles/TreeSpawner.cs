using System.Collections;
using UnityEngine;

public class TreeSpawner : MonoBehaviour, ISpawner
{
    public GameObject TreePrefab;
    public int MinTreeNum = 1;
    public int MaxTreeNum = 10;
    public Transform GrassTransform;

    private int _count;
    private float _position;
    private int _tileLengthHalf;
    private float _distance;
    private void Awake()
    {
        _count = Random.Range(MinTreeNum, MaxTreeNum);
        _tileLengthHalf = (int)(GrassTransform.localScale.x / (2 * _count));
        _distance = 2 * Random.Range(1, _tileLengthHalf);
        _position = _distance + 1;
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (_count != 0)
        {
            GameObject tree = Instantiate(TreePrefab, gameObject.transform.position + _position * transform.forward, gameObject.transform.rotation);
            tree.transform.SetParent(transform);

            _distance = 2 * Random.Range(1, _tileLengthHalf);
            _position += _distance;
            
            yield return --_count;
        }
    }
}
