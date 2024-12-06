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

    // declared in unity
    public GameObject projectilePrefab;

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
}
