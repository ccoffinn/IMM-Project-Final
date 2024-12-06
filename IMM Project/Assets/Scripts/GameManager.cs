// Sarah Scott & Gabriella SECOND NAME

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    // track if the game is running
    public bool isGameActive;
    // list of hazards
    public List<GameObject> hazards;
    // hazard spawn range
    private float spawnRange = 10.0f;
    // HUD elements
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI healthText;
    // Manage health and score
    public int health;
    private int score;
    

    // Start is called before the first frame update
    void Start()
    {
        StartGame(); // TODO remove later, handled by button
    }

    public void StartGame() {
        // set the game as active
        isGameActive = true;
        
        // set starting score to 0
        score = 0;
        UpdateScore(0);

        // set starting health to 100
        health = 100;
        healthText.text = "Health: " + health;

        // start the spawn manager
        StartCoroutine(SpawnManager());
    }

    public void EndGame() {
        isGameActive = false;

        // display game over text
        gameOverText.gameObject.SetActive(true);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // spawn a random enemy from list every 1 seconds
    IEnumerator SpawnManager() {
        while (isGameActive) {
            yield return new WaitForSeconds(1);
            int index = Random.Range(0, hazards.Count);
             Instantiate(hazards[index], SpawnPosition(), hazards[index].transform.rotation);
        }
        
    }

    // generate random position for enemy spawning
    private Vector3 SpawnPosition() {
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(10, 0.7f, spawnPosZ);

        return randomPos;
    }

    public void UpdateScore(int scoreToAdd) 
    { 
        score += scoreToAdd; 
        scoreText.text = "Score: " + score; // update HUD
    }

    public void UpdateHealth(int healthToRemove)
    { 
        health -= healthToRemove; 
        healthText.text = "Health: " + health; // update HUD

        // end the game if health reaches 0
        if (health <= 0) {
            EndGame();
        }
    }
}
