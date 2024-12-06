// Sarah Scott

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float horizontalInput;
    public float speed = 10.0f;
    private float zRange = 10;
    private float xRange = -20;
    private float cooldown = 0.5f;
    private Vector3 offset = new Vector3(1,0,0);
    public GameManager gameManager;
    private int tempValue = 10; // placeholder value TODO replace with method to calculate 

    // declared in unity
    public GameObject projectilePrefab;

    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                cooldown = 0.5f;
                // TODO particle system here
                // TODO sound here
            }

            // reset cooldown
            if (cooldown >= 0) {
                cooldown -= Time.deltaTime;
            }

            // move left or right
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.back * horizontalInput * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.CompareTag("Player")) {
            PlayerHazardCollision(tempValue, other);
        }
    }

    // when a hazard collides with player
    private void PlayerHazardCollision(int healthLoss, Collision other) {
        gameManager.UpdateHealth(healthLoss);
        
        if (gameManager.health > 0) {
            Destroy(other.gameObject);
        }
        else {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
