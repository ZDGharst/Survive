using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Rigidbody rigidbody;
    public ParticleSystem explosionParticle;

    private float initialSpeed = 8.0f;
    private float maxSpeed = 40.0f;
    private float speed;

    private int horizontalBounds = 100;
    private int verticalBounds = 120;


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
        if(other.gameObject.CompareTag("Environment"))
        {
            Destroy(other.gameObject);
        }

        else if(other.gameObject.CompareTag("Zombie"))
        {
            other.gameObject.GetComponent<Zombie>().health--;
        }

        else if(other.gameObject.CompareTag("Tombstone"))
        {
            other.gameObject.GetComponent<Tombstone>().CreateZombie();

            if(--other.gameObject.GetComponent<Tombstone>().health <= 0)
            {
                Destroy(other.gameObject);
                GameManager.tombstonesDestroyed++;
            }
        }

        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        Destroy(gameObject);
    }
}
