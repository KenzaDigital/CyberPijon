using System.Collections;
using UnityEngine;

public class PijonController : MonoBehaviour
{
    public Rigidbody2D rb;
    private bool isDead = false;
    [SerializeField]
    private float pijonUpForce = 5f;

    private GameManager gameManager;
    private Vector3 startPosition;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position; // Enregistrer la position de départ
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * pijonUpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");
        gameManager.PlayerTakeDamage(1);
        StartCoroutine(Flash());

        if (gameManager.GetCurrentHealth() <= 0)
        {
            isDead = true;
            gameManager.GameOver();
            // Appeler la méthode RespawnPlayer après un délai pour simuler le respawn
            Invoke("RespawnPlayer", 2f);
        }
    }

    private IEnumerator Flash()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Respawn()
    {
        Debug.Log("Respawning...");
        isDead = false;
        transform.position = startPosition; // Réinitialiser la position
        rb.linearVelocity = Vector2.zero; // Réinitialiser la vélocité
    }

    private void RespawnPlayer()
    {
        gameManager.RespawnPlayer(this);
    }
}
    
    

