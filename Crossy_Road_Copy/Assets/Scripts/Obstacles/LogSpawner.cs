using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour, ISpawner
{
    public GameObject[] LogPrefab = new GameObject[3];
    public float MinSpawnCooltime = 1f;
    public float MaxSpawnCooltime = 3f;

    private GameObject _logPrefab;
    private float _spawnCooltime;
    private void Awake()
    {
        _spawnCooltime = Random.Range(MinSpawnCooltime, MaxSpawnCooltime);
        _logPrefab = LogPrefab[Random.Range(0, 3)];
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (true)
        {
            GameObject log = Instantiate(_logPrefab, gameObject.transform.position, gameObject.transform.rotation);
            log.transform.SetParent(transform);
            
            yield return new WaitForSeconds(_spawnCooltime);
        }
    }
}
