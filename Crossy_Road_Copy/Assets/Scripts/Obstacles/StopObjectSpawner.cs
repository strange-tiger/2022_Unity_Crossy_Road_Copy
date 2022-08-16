using System.Collections;
using UnityEngine;

public class StopObjectSpawner : MonoBehaviour, ISpawner
{
    public GameObject Prefab;
    public int MinNum = 1;
    public int MaxNum = 5;
    public float TileLength = 60f;
    public float InitPosition = 0f;
    public float Offset = 10f;

    private int _count;
    private float _position;
    private int _tileLengthHalf;
    private float _distance = 2f;
    private void Awake()
    {
        _count = Random.Range(MinNum, MaxNum);
        _tileLengthHalf = (int)((TileLength - Offset) / (2 * _count));
        _position = InitPosition + Offset;

        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (_count != 0)
        {
            GameObject tree = Instantiate(Prefab, transform.position + _position * transform.forward, transform.rotation);
            tree.transform.SetParent(transform);

            _distance = 2 * Random.Range(1, _tileLengthHalf);
            _position += _distance;

            yield return --_count;
        }
    }
}
