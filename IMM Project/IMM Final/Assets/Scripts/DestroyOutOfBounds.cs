// Sarah Scott

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    
    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);

        // kill player if an "InstaDeath" object hits this collider
        if (other.gameObject.CompareTag("InstaDeath")) {
            gameManager.UpdateHealth(100);
            Destroy(player);
        }
    }
}
