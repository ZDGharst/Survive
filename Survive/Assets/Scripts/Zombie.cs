using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private float speed = 5.0f;
    private GameObject player;
    private Vector3 direction;

    void Start()
    {
        player = GameObject.Find("Turret");
        direction = (player.transform.position - transform.position).normalized;
        
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
