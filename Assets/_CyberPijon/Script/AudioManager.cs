using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private readonly int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // M�thode pour arr�ter la musique
    public void StopMusic()
    {
        if (currentHealth <= 0)
        {
            audioSource.Stop();
        }
    }
}
