using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private GameObject player;
    private Vector3 direction;
    private Animator animator;
    private AudioSource attackSound;

    public float speed = 4.0f;
    public int health;
    private float attackRate = 1.8f;
    private float attackCooldown = 0.5f;
    private float attackDamage = 1.0f;

    void Start()
    {
        /* A zombie can survive between 1-3 hits, and if its bulkier, it's slower. */
        health = Random.Range(1, 4);
        speed -= health;
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Turret");
        attackSound = GetComponent<AudioSource>();
        attackSound.volume = 0.75f * PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
    }

    void Update()
    {
        if(health > 0)
        {
            direction = player.transform.position - transform.position;

            if (direction.magnitude > 2.5f)
            {
                MoveTowardTurret();
            }
            else
            {
                AttackTurret();
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
        GameManager.zombiesKilled++;
    }

    void MoveTowardTurret()
    {
        transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        transform.position += direction.normalized * speed * Time.deltaTime;
        animator.SetFloat("MoveSpeed", speed * 2.0f);
    }

    void AttackTurret()
    {
        animator.SetFloat("MoveSpeed", 0.0f);
        animator.SetTrigger("Attack");
        if(attackCooldown <= 0)
        {
            player.GetComponent<Turret>().TakeDamage(attackDamage);
            attackCooldown = attackRate;
            attackSound.Play();
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }
}
