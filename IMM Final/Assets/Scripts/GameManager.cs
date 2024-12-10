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
    // player object
    public GameObject player;

    // HUD elements
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI titleText;
    public Button restartButton;
    public Button startButton;
    public Button controlsButton;
    public Button gitButton;

    // Manage health and score
    private int health = 50;
    private int score = 0;
    private float difficulty;

    // Game over sound
    private AudioSource gameOverAudio;
    public AudioClip gameOverSound;
    public AudioClip startSound;    

    // Start is called before the first frame update
    void Start()
    {
        gameOverAudio = GetComponent<AudioSource>();
    }

    public void StartGame() {
        // set the game as active
        isGameActive = true;

        // play sound
        gameOverAudio.PlayOneShot(startSound, 1.0f);
        
        // set starting score
        UpdateScore(score);

        // set starting health
        healthText.text = "Health: " + health;

        // remove main menu UI
        titleText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        controlsButton.gameObject.SetActive(false);
        gitButton.gameObject.SetActive(false);

        // add game UI and player
        healthText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        
        // start the spawn manager
        StartCoroutine(SpawnManager());
    }

    public void EndGame() {
        // set game as inactive
        isGameActive = false;

        // play game over sound
        gameOverAudio.PlayOneShot(gameOverSound, 1.0f);

        // display game over text
        gameOverText.gameObject.SetActive(true);
        // display restart button
        restartButton.gameObject.SetActive(true);
        // hide health
        healthText.gameObject.SetActive(false);

    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // spawn a random enemy from list every 1 seconds
    IEnumerator SpawnManager() {
        while (isGameActive) {
            // difficulty decides spawn rate
            yield return new WaitForSeconds(calculateDifficulty(score));
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

    // calculate health loss based on what hazard player collides with
    public static int CalculateHealthLoss(Collision other) {
        int lifeLoss = 0;
        if (other.gameObject.CompareTag("InstaDeath")) {
            lifeLoss = 100;
        }
        else if (other.gameObject.CompareTag("Hazard")) {
            lifeLoss = 10;
        }
        else if (other.gameObject.CompareTag("HazardMoving")) {
            lifeLoss = 20;
        }

        return lifeLoss;
    }

    // calculate when projectile hits hazard
    public static int CalculateScoreGain(Collision other) {
        int scoreGain = 0;
        if (other.gameObject.CompareTag("InstaDeath")) {
            scoreGain = 30;
        }
        else if (other.gameObject.CompareTag("Hazard")) {
            scoreGain = 5;
        }
        else if (other.gameObject.CompareTag("HazardMoving")) {
            scoreGain = 15;
        }
        else if (other.gameObject.CompareTag("PowerUp")) {
            scoreGain = -10;
        }

        return scoreGain;
    }

    // set difficulty based on score
    public float calculateDifficulty(int currentScore) {
        if (currentScore < 200) {
            difficulty = 1.0f;
        }
        else if (currentScore < 500 && currentScore > 199) {
            difficulty = 0.8f;
        }
        else if (currentScore < 800 && currentScore > 499) {
            difficulty = 0.6f;
        }
        else if (currentScore < 1100 && currentScore > 799) {
            difficulty = 0.4f;
        }
        else if (currentScore < 1500 && currentScore > 1099) {
            difficulty = 0.2f;
        }

        // impossible difficulty effectively ending the game
        else if (currentScore > 5000) {
            difficulty = 0.05f;
        }

        return difficulty;
    }

    // method for github button on main menu
    public void OpenGitHub() {
        Application.OpenURL("https://github.com/ccoffinn/IMM-Project-Final");
    }

    // method for controls button on main menu
    public void OpenControls(){

    }

    public int getHealth() {
        return health;
    }

    public int getScore() {
        return score;
    }

    public void setHealth(int newHealth) {
        this.health = newHealth;
    }

    public void setScore(int newScore) {
        this.score = newScore;
    }
}
