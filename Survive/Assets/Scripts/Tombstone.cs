using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombstone : MonoBehaviour
{
    public GameObject zombie;
    public int health = 5;

    void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    void Update()
    {
    }

    public void CreateZombie()
    {
        Instantiate(zombie, transform.position, zombie.transform.rotation);
    }

    IEnumerator SpawnZombies()
    {
        while(true)
        {
            CreateZombie();
            yield return new WaitForSeconds(6);
        }
    }
}
