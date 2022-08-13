using System.Collections;
using UnityEngine;

public class MovingObjectSpawner : MonoBehaviour, ISpawner
{
    public const int PrefabKindNum = 4;
    
    public GameObject[] PrefabKinds = new GameObject[PrefabKindNum];
    public float TileSize = 60f;
    public float MinSpawnCooltime = 1.5f;
    public float MaxSpawnCooltime = 5f;

    private GameObject[][] _prefabs = new GameObject[PrefabKindNum][];
    private int _prefabIndex = 0;
    private int[] _nextSpawnIndex = new int[PrefabKindNum];
    private int _maxPrefabCount = 0;
    private float _spawnCooltime;
    private float _minPrefabSpeed;

    private void Awake()
    {
        _minPrefabSpeed = PrefabKinds[0].GetComponent<MovingObject>().Speed;

        SetMaxPrefabCount();

        for (int j = 0; j < PrefabKindNum; ++j)
        {
            _prefabs[j] = new GameObject[_maxPrefabCount];
            _nextSpawnIndex[j] = 0;

            for (int i = 0; i < _maxPrefabCount; ++i)
            {
                _prefabs[j][i] = Instantiate(PrefabKinds[j], transform.position, transform.rotation);
                _prefabs[j][i].transform.SetParent(transform);
                _prefabs[j][i].SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        SetSpawnCooltime();
        SetPrefab();
        
        StartCoroutine(Spawn());
    }

    private void SetSpawnCooltime()
    {
        _spawnCooltime = Random.Range(MinSpawnCooltime, MaxSpawnCooltime);
        
        for (int j = 0; j < PrefabKindNum; ++j)
        {
            for (int i = 0; i < _maxPrefabCount; ++i)
            {
                _prefabs[j][i].SetActive(false);
            }
            _nextSpawnIndex[j] = 0;
        }
    }

    private void SetPrefab()
    {
        _prefabIndex = Random.Range(0, PrefabKinds.Length);
    }

    private void SetMaxPrefabCount()
    {
        _maxPrefabCount = (int)(TileSize / (_minPrefabSpeed * MinSpawnCooltime)) + 1;
    }

    public IEnumerator Spawn()
    {
        while (true)
        {
            GameObject currentObject = _prefabs[_prefabIndex][_nextSpawnIndex[_prefabIndex]];
            currentObject.SetActive(false);
            currentObject.transform.position = transform.position;
            currentObject.SetActive(true);
            ++_nextSpawnIndex[_prefabIndex];
            _nextSpawnIndex[_prefabIndex] %= _maxPrefabCount;

            yield return new WaitForSeconds(_spawnCooltime);
        }
    }
}
