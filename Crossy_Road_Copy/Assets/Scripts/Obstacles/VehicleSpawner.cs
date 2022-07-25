using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour, ISpawner
{
    public GameObject VehiclePrefab;
    public float SpawnCooltime = 2f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (true)
        {
            GameObject vehicle = Instantiate(VehiclePrefab, gameObject.transform.position, gameObject.transform.rotation);
            
            yield return new WaitForSeconds(SpawnCooltime);
        }
    }
}
