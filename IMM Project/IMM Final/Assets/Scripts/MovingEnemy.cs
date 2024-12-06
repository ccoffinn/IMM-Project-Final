using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public GameManager gameManager;
    private int tempValue = 10; // placeholder value TODO replace with method to calculate 
    
    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // method for handling collisions
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("HazardMoving") || other.gameObject.CompareTag("Projectile")) {
            ProjectileHazardCollision(tempValue, other);
        }
    }


    // when projectile collides with hazard
    private void ProjectileHazardCollision(int scoreGain, Collision other) {
        gameManager.UpdateScore(scoreGain);
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
