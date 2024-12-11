// Sarah Scott & Emmanuel Adenola

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private float speed = 10.0f;
    private Vector3 direction = new Vector3(1,0,0);
    public GameManager gameManager;
    // audio and particle effects
    public AudioSource projectileAudio;
    public AudioClip hitSound;
    
    

    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        projectileAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed, Space.World);
    }

    // method for handling collisions
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PowerUp" ) || other.gameObject.CompareTag("Hazard") || 
            other.gameObject.CompareTag("HazardMoving") || other.gameObject.CompareTag("InstaDeath")) {
            ProjectileHazardCollision(GameManager.CalculateScoreGain(other), other);
            projectileAudio.PlayOneShot(hitSound, 1.5f);
        }
    }

    // when projectile collides with hazard
    private void ProjectileHazardCollision(int scoreGain, Collision other) {
        gameManager.UpdateScore(scoreGain);
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
