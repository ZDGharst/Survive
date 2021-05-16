using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed = 4.0f;
    private GameObject player;
    private Vector3 direction;
    private Animator animator;

    public int health;

    void Start()
    {
        /* A zombie can survive between 1-3 hits, and if its bulkier, it's slower. */
        health = Random.Range(1, 4);
        speed -= health;
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Turret");
    }

    void Update()
    {
        if(health > 0)
        {
            direction = player.transform.position - transform.position;

            if (direction.magnitude > 2.5f)
            {
                transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
                transform.position += direction.normalized * speed * Time.deltaTime;
                animator.SetFloat("MoveSpeed", speed * 2.0f);
            }
            else
            {
                animator.SetFloat("MoveSpeed", 0.0f);
                animator.SetTrigger("Attack");
                player.GetComponent<Turret>().health -= Time.deltaTime;

                if(player.GetComponent<Turret>().health < 0)
                {
                    print("Dead");
                }
            }
        }
        else
        {
            StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        animator.SetFloat("MoveSpeed", 0.0f);
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
