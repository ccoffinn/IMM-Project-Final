// Sarah Scott & Emmanuel SECOND NAME

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    public GameManager gameManager;
    private int tempValue = 10; // placeholder value TODO replace with method to calculate 
    
    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    // method for handling collisions
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) {
            PlayerHazardCollision(tempValue, other);
        }
        else if (other.gameObject.CompareTag("Hazard")) {
            ProjectileHazardCollision(tempValue, other);
        }
        else if (other.gameObject.CompareTag("HazardMoving")) {
            ProjectileHazardCollision(tempValue, other);
        }
    }

    // when a hazard collides with player
    private void PlayerHazardCollision(int healthLoss, Collision other) {
        gameManager.UpdateHealth(healthLoss);
        
        if (gameManager.health > 0) {
            Destroy(gameObject);
        }
        else {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    // when projectile collides with hazard
    private void ProjectileHazardCollision(int scoreGain, Collision other) {
        gameManager.UpdateScore(scoreGain);
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
