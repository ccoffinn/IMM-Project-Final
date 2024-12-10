using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameManager gameManager;
    
    
    void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
