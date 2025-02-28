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
    private TextMeshProUGUI scoreText; // R�f�rence au composant TextMeshPro pour afficher le score

    [SerializeField]
    public int currentHealth = 3; // Variable pour stocker la sant� actuelle

    [SerializeField]
    private int maxHealth = 3; // Variable pour stocker la sant� maximale

    [SerializeField]
    private GameObject heartPrefab; // R�f�rence au GameObject du c�ur

    [SerializeField]
    private Transform heartContainer; // R�f�rence au conteneur des c�urs

    [SerializeField]
    private GameObject gameOverPanel; // R�f�rence au panneau de Game Over

    private PijonController pijonController;
    private AudioManager audioManager;
    private bool isGameOver = false; // Variable pour indiquer si le jeu est termin�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialiser le score et mettre � jour le texte
        score = 0;
        UpdateScoreText();
        InitializeHearts();
        UpdateHearts();
        gameOverPanel.SetActive(false); // Assurez-vous que le panneau de Game Over est d�sactiv� au d�but

        pijonController = GameObject.FindGameObjectWithTag("Player").GetComponent<PijonController>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            // Exemple d'incr�mentation du score (� remplacer par votre logique de jeu)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                score++;
                UpdateScoreText();
            }
        }
    }

    // M�thode pour mettre � jour le texte du score
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // M�thode pour initialiser les c�urs
    private void InitializeHearts()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            Instantiate(heartPrefab, heartContainer);
        }
    }

    // M�thode pour mettre � jour l'affichage des c�urs
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

    // M�thode pour g�rer la perte de vie
    public void PlayerTakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHearts();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    // M�thode pour g�rer la fin de la partie
    public void GameOver()
    {
        Debug.Log("Game Over!");
        isGameOver = true; // Indiquer que le jeu est termin�
        gameOverPanel.SetActive(true); // Afficher le panneau de Game Over
        Time.timeScale = 0;
        audioManager.StopMusic(); // Arr�ter la musique
    }

    // M�thode pour r�initialiser la sant� du personnage
    public void ResetHealth()
    {
        Debug.Log("Resetting health...");
        currentHealth = maxHealth;
        UpdateHearts();
    }

    // M�thode pour r�initialiser le score
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    // M�thode pour recharger la sc�ne
    public void RestartGame()
    {
        Debug.Log("Restarting game...");
        Time.timeScale = 1; // R�initialiser le temps
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recharger la sc�ne actuelle
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
        isGameOver = false; // R�initialiser l'�tat de fin de jeu
        pijonController.Respawn();
    }
}

