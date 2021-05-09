﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Rigidbody rigidbody;

    private float initialSpeed = 8.0f;
    private float maxSpeed = 40.0f;
    private float speed;

    private int horizontalBounds = 100;
    private int verticalBounds = 120;

    public ParticleSystem explosionParticle;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        speed = initialSpeed;
    }

    void Update()
    {
        /* Transform up, not forward, because of model axis */
        rigidbody.velocity = transform.up * speed;
        transform.Rotate(new Vector3(0, speed, 0));

        if(transform.position.x < -horizontalBounds ||
           transform.position.x > horizontalBounds ||
           transform.position.z < -verticalBounds ||
           transform.position.z > verticalBounds)
        {
            Destroy(gameObject);
        }

        if(speed < maxSpeed)
        {
            speed += 4;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Ground"))
        {
            Destroy(other.gameObject);
        }

        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        Destroy(gameObject);
    }
}