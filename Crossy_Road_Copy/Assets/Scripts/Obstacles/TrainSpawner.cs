using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour, ISpawner
{
    public GameObject TrainPrefab;
    public GameObject TrainLight;
    public float MinSpawnCooltime = 4f;
    public float MaxSpawnCooltime = 8f;

    private float _spawnCooltime;
    private float _redLightTime = 2f;
    private float _traingIsGoing = 1f;
    private void Awake()
    {
        _spawnCooltime = Random.Range(MinSpawnCooltime, MaxSpawnCooltime);
        StartCoroutine(Spawn());
    }
    
    public IEnumerator Spawn()
    {
        while (true)
        {
            TrainLight.SetActive(true);

            yield return new WaitForSeconds(_redLightTime);

            GameObject vehicle = Instantiate(TrainPrefab, gameObject.transform.position, gameObject.transform.rotation);
            vehicle.transform.SetParent(transform);

            yield return new WaitForSeconds(_traingIsGoing);

            TrainLight.SetActive(false);
            
            yield return new WaitForSeconds(_spawnCooltime);
        }
    }

}
