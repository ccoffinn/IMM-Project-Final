// Sarah Scott & Emmanuel SECOND NAME

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyControllerXt : MonoBehaviour
{
    public float moveSpeed = 15.0f; // Movement speed of the enemy
    private Vector2 moveDown = new Vector2(-1,0); // move all enemies towards player
    private Vector3 moveLeft = new Vector3(0,0,2); // move moving hazards left
    private Vector3 moveRight = new Vector3(0,0,-2); // move moving hazards right

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoveHazards());
    }

    // Method to move enemy in the chosen direction
    IEnumerator MoveHazards() {

        transform.Translate(moveDown * moveSpeed * Time.deltaTime);

        if (gameObject.CompareTag("HazardMoving")) {
            int index = Random.Range(0,2);
            int timeIndex = Random.Range(3,5);
            
            // move left or right randomly
            if (index == 0) {
                yield return new WaitForSeconds(timeIndex);
                transform.Translate(moveLeft * 6 * Time.deltaTime);
            }
            else if (index == 1) {
                yield return new WaitForSeconds(timeIndex);
                transform.Translate(moveRight * 6 * Time.deltaTime);
            }
        }
    }
}

