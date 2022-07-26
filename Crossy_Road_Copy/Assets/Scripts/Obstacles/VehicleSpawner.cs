using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour, ISpawner
{
    public GameObject[] VehiclePrefab = new GameObject[4];
    public float MinSpawnCooltime = 1.5f;
    public float MaxSpawnCooltime = 5f;

    private GameObject _vehiclePrefab; 
    private float _spawnCooltime;
    private void Awake()
    {
        _spawnCooltime = Random.Range(MinSpawnCooltime, MaxSpawnCooltime);
        _vehiclePrefab = VehiclePrefab[Random.Range(0, 4)];
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (true)
        {
            GameObject vehicle = Instantiate(_vehiclePrefab, gameObject.transform.position, gameObject.transform.rotation);
            vehicle.transform.SetParent(transform);
            
            yield return new WaitForSeconds(_spawnCooltime);
        }
    }
}
