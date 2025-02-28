using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0; // Variable pour stocker le score

    [SerializeField]
    private TextMeshProUGUI scoreText; // Référence au composant TextMeshPro pour afficher le score

    [SerializeField]
    public int currentHealth = 3; // Variable pour stocker la santé actuelle

    [SerializeField]
    private int maxHealth = 3; // Variable pour stocker la santé maximale

    [SerializeField]
    private GameObject heartPrefab; // Référence au GameObject du cœur

    [SerializeField]
    private Transform heartContainer; // Référence au conteneur des cœurs

    [SerializeField]
    private GameObject gameOverPanel; // Référence au panneau de Game Over

    private PijonController pijonController;
    private AudioManager audioManager;
    private bool isGameOver = false; // Variable pour indiquer si le jeu est terminé

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialiser le score et mettre à jour le texte
        score = 0;
        UpdateScoreText();
        InitializeHearts();
        UpdateHearts();
        gameOverPanel.SetActive(false); // Assurez-vous que le panneau de Game Over est désactivé au début

        pijonController = GameObject.FindGameObjectWithTag("Player").GetComponent<PijonController>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            // Exemple d'incrémentation du score (à remplacer par votre logique de jeu)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                score++;
                UpdateScoreText();
            }
        }
    }

    // Méthode pour mettre à jour le texte du score
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // Méthode pour initialiser les cœurs
    private void InitializeHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            Instantiate(heartPrefab, heartContainer);
        }
    }

    // Méthode pour mettre à jour l'affichage des cœurs
    private void UpdateHearts()
    {
        for (int i = 0; i < heartContainer.childCount; i++)
        {
            if (i < currentHealth)
            {
                heartContainer.GetChild(i).gameObject.SetActive(true);
                Debug.Log("Heart " + i + " is active");
            }
            else
            {
                heartContainer.GetChild(i).gameObject.SetActive(false);
                Debug.Log("Heart " + i + " is desactive");
            }
        }
    }

    // Méthode pour gérer la perte de vie
    public void PlayerTakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHearts();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    // Méthode pour gérer la fin de la partie
    public void GameOver()
    {
        Debug.Log("Game Over!");
        isGameOver = true; // Indiquer que le jeu est terminé
        gameOverPanel.SetActive(true); // Afficher le panneau de Game Over
        Time.timeScale = 0;
        audioManager.StopMusic(); // Arrêter la musique
    }

    // Méthode pour réinitialiser la santé du personnage
    public void ResetHealth()
    {
        Debug.Log("Resetting health...");
        currentHealth = maxHealth;
        UpdateHearts();
    }

    // Méthode pour réinitialiser le score
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    // Méthode pour recharger la scène
    public void RestartGame()
    {
        Debug.Log("Restarting game...");
        Time.timeScale = 1; // Réinitialiser le temps
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recharger la scène actuelle
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void RespawnPlayer(PijonController pijonController)
    {
        Debug.Log("Respawning player...");
        ResetHealth();
        ResetScore();
        isGameOver = false; // Réinitialiser l'état de fin de jeu
        pijonController.Respawn();
    }
}

