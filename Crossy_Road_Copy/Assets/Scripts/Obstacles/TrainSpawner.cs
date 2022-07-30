using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour, ISpawner
{
    public GameObject TrainPrefab;
    public GameObject TrainLight;
    public float MinSpawnCooltime = 4f;
    public float MaxSpawnCooltime = 8f;

    private GameObject _train;
    private float _spawnCooltime;
    private float _redLightTime = 2f;
    private float _trainIsGoing = 1f;
    private void Awake()
    {
        _spawnCooltime = Random.Range(MinSpawnCooltime, MaxSpawnCooltime);
        
        _train = Instantiate(TrainPrefab, transform.position, transform.rotation);
        _train.transform.SetParent(transform);
        _train.SetActive(false);

        StartCoroutine(Spawn());
    }
    
    public IEnumerator Spawn()
    {
        while (true)
        {
            TrainLight.SetActive(true);

            yield return new WaitForSeconds(_redLightTime);

            _train.transform.position = transform.position;
            _train.SetActive(true);

            yield return new WaitForSeconds(_trainIsGoing);

            TrainLight.SetActive(false);
            
            yield return new WaitForSeconds(_spawnCooltime);
        }
    }

}
