using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    private Camera camera;
    private GameObject head;
    public GameManager gameManager;
    private AudioSource attackSound;
    public Image healthBar;

    private bool leftBarrel = true;
    private float barrelXPosLeft = -0.19f;
    private float barrelXPosRight = 0.25f;
    private float barrelYPos = 2.44f;
    private float barrelZPos = 1.60f;

    public float health = 5.0f;
    private float cooldown = 0.5f;

    public GameObject missilePrefab;
    public ParticleSystem leftBarrelParticle;
    public ParticleSystem rightBarrelParticle;

    void Start()
    {
        camera = Camera.main;
        head = GameObject.Find("Head");

        attackSound = GetComponent<AudioSource>();
        attackSound.volume = 0.75f * PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
    }

    void Update()
    {
        /* Have the head of the turret look in the direction of the mouse
         * https://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html */
        Vector3 input = Input.mousePosition;
        Vector3 mousePosition = camera.ScreenToWorldPoint(new Vector3(input.x, input.y, camera.transform.position.y));
        mousePosition.y = 0;
        head.transform.LookAt(mousePosition + Vector3.up * head.transform.position.y);
        
        cooldown -= Time.deltaTime;
        if(Input.GetMouseButton(0) && cooldown <= 0)
        {
            Attack();
        }
    }

    private void Attack()
    {
        attackSound.Play();

        float barrelXPos = barrelXPosRight;
        /* Alternate firing between left and right barrel. */
        if(leftBarrel)
        {
            barrelXPos = barrelXPosLeft;
            leftBarrelParticle.Play();
        }
        else
        {
            rightBarrelParticle.Play();
        }
        leftBarrel = !leftBarrel;
        cooldown = 0.5f;

        /* Calculate position of projectile and instantiate it. */
        float headDirection = head.transform.rotation.eulerAngles.y + 90;
        Vector3 headPosition = head.transform.position;
        Vector3 barrel = head.transform.rotation * new Vector3(barrelXPos, barrelYPos, barrelZPos);
        Quaternion missileRotation = Quaternion.Euler(0.0f, headDirection, 90.0f);

        Instantiate(missilePrefab, barrel, missileRotation);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        healthBar.rectTransform.sizeDelta = new Vector2(health * 40.0f, 40.0f);
        healthBar.transform.localPosition = new Vector3((health -5) * 20, 0.0f, 0.0f);

        if(health <= 0)
        {
            GameManager.gameOver = true;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
