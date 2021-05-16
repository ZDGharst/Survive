using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject tombstone;

    private int wave = 1;
    private int horizontalBounds = 60;
    private int verticalBounds = 80;
    private int safetyZone = 15;
    private float waveTimer = 60.0f;

    void Start()
    {
        GenerateTombstones();
    }

    void Update()
    {
        waveTimer -= Time.deltaTime;

        if(waveTimer <= 0)
        {
            GenerateTombstones();
            waveTimer = 60.0f;
        }
    }

    void GenerateTombstones()
    {
        int enemiesToSpawn = wave * wave / 4 + 1;
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(tombstone, GenerateSpawnPosition(), tombstone.transform.rotation);
        }
        wave++;
    }

    Vector3 GenerateSpawnPosition()
    {
        /* Generate a spawn position between the safety zone and right/top. */
        float spawnPosX = Random.Range(safetyZone, horizontalBounds);
        float spawnPosZ = Random.Range(safetyZone, verticalBounds);

        /* 50% of the time, make it spawn on the left and/or bottom. */
        if(Random.value >= 0.5)
        {
            spawnPosX *= -1;
        }
        if(Random.value >= 0.5)
        {
            spawnPosZ *= -1;
        }

        Vector3 randomPos = new Vector3(spawnPosX, 0.0f, spawnPosZ);
        return randomPos;
    }
}
