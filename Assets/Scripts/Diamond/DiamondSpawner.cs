using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    public float spawnTime = 1;
    public float spawnDelay = 30;
    public bool stopSpawning = false;

    public GameObject diamond;

    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-20, 20), 200, (Random.Range(-20, 20)));
        Instantiate(diamond, randomSpawnPosition, Quaternion.identity);

        if (stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
}