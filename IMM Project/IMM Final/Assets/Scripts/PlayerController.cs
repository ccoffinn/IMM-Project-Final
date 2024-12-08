// Sarah Scott

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float horizontalInput;
    private float speed = 10.0f;
    private float zRange = 10;
    private float xRange = -20;
    private float cooldown;
    private Vector3 offset = new Vector3(1,0,0);
    public GameManager gameManager;
    public bool isPowerUp = false;
    // player audio
    private AudioSource playerAudio;

    // declared in unity
    public GameObject projectilePrefab;
    //public ParticleSystem projectilParticle;
    //public ParticleSystem damageParticle;
    //public AudioClip projectileSound;
    //public AudioClip damageSound;
    //public AudioClip powerupSound;


    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
            // left border
            if (transform.position.z < -zRange) {
                transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
            }

            // right border
            if (transform.position.z > zRange) {
                transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
            }

            // keep player on the x plane
            if (transform.position.x != xRange) {
                transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
            }

            // launch projectile from player
            if (Input.GetKeyDown(KeyCode.Space) && cooldown <= 0) {
                Instantiate(projectilePrefab, transform.position + offset, projectilePrefab.transform.rotation);
                
                // set cooldown
                cooldown = CalculateCooldown();
                
                // audio and particles
                //projectilParticle.Play();
                //playerAudio.PlayOneShot(projectileSound, 1.0f);
            }

            // reset cooldown
            if (cooldown >= 0) {
                cooldown -= Time.deltaTime;
            }

            // move left or right
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.back * horizontalInput * Time.deltaTime * speed, Space.World);
    }

    // collision handler
    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.CompareTag("Player")) {
            PlayerHazardCollision(GameManager.CalculateHealthLoss(other), other);
            
            
            // only play damage effect when collision with non powerup
            if (other.gameObject.CompareTag("Hazard") || other.gameObject.CompareTag("HazardMoving")) {
                //damageParticle.Play();
                //playerAudio.PlayOneShot(damageSound, 1.0f);
            }
            // if collide with instaDeath, deathSound will play instead (handled by gameManager)
            if (other.gameObject.CompareTag("InstaDeath")) {
                //damageParticle.Play();
            }
        }
        
        // when player collide with powerup
        if (other.gameObject.CompareTag("PowerUp")) {
            isPowerUp = true;
            //playerAudio.PlayOneShot(powerupSound, 1.0f);
            Invoke("ResetPowerUp", 5.0f);
        }
    }

    // when a hazard collides with player
    private void PlayerHazardCollision(int healthLoss, Collision other) {
        gameManager.UpdateHealth(healthLoss);
        
        if (gameManager.getHealth() > 0) {
            Destroy(other.gameObject);
        }
        // destory both player and object if health is below 0
        else {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    // calculate cooldown
    public float CalculateCooldown() {
        if (isPowerUp) {
            cooldown = 0.2f;
        }
        else {
            cooldown = 0.5f;
        }

        return cooldown;
    }

    // reset powerUp status
    public void ResetPowerUp() {
        isPowerUp = false;
    }
}
