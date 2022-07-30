using System.Collections;
using UnityEngine;

public class MovingObjectSpawner : MonoBehaviour, ISpawner
{
    public GameObject[] PrefabKinds = new GameObject[4];
    public float TileSize = 60f;
    public float MinSpawnCooltime = 1.5f;
    public float MaxSpawnCooltime = 5f;

    private GameObject _prefab;
    private GameObject[] _prefabs;
    private int _nextSpawnIndex = 0;
    private int _maxPrefabCount = 0;
    private float _spawnCooltime;
    private float _minPrefabSpeed;

    private void Awake()
    {
        _minPrefabSpeed = PrefabKinds[0].GetComponent<MovingObject>().Speed;

        SetSpawnCooltime();
        SetPrefab();
        SetMaxPrefabCount();

        _prefabs = new GameObject[_maxPrefabCount];
        for (int i = 0; i < _maxPrefabCount; ++i)
        {
            _prefabs[i] = Instantiate(_prefab, transform.position, transform.rotation);
            _prefabs[i].transform.SetParent(transform);
            _prefabs[i].SetActive(false);
        }

        StartCoroutine(Spawn());
    }

    private void SetSpawnCooltime()
    {
        _spawnCooltime = Random.Range(MinSpawnCooltime, MaxSpawnCooltime);
    }

    private void SetPrefab()
    {
        _prefab = PrefabKinds[Random.Range(0, PrefabKinds.Length)];
    }

    private void SetMaxPrefabCount()
    {
        _maxPrefabCount = (int)(TileSize / (_minPrefabSpeed * MinSpawnCooltime)) + 1;
    }

    public IEnumerator Spawn()
    {
        while (true)
        {
            GameObject currentObject = _prefabs[_nextSpawnIndex];
            currentObject.SetActive(false);
            currentObject.transform.position = transform.position;
            currentObject.SetActive(true);
            ++_nextSpawnIndex;
            _nextSpawnIndex %= _maxPrefabCount;

            yield return new WaitForSeconds(_spawnCooltime);
        }
    }
}
