using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour, ISpawner
{
    public GameObject LogPrefab;
    public float SpawnCooltime = 2f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        while (true)
        {
            GameObject vehicle = Instantiate(LogPrefab, gameObject.transform.position, gameObject.transform.rotation);

            yield return new WaitForSeconds(SpawnCooltime);
        }
    }
}
