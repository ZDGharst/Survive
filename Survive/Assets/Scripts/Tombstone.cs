using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombstone : MonoBehaviour
{
    public GameObject zombie;

    void Start()
    {
        StartCoroutine(SpawnZombie());
    }

    void Update()
    {
    }

    IEnumerator SpawnZombie()
    {
        while(true)
        {
            Instantiate(zombie, transform.position, zombie.transform.rotation);
            yield return new WaitForSeconds(6);
        }

    }
}
