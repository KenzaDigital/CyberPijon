using System.Threading;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject pipe;

    [SerializeField]
    private float spawnRate = 2;

    [SerializeField]
    private float heightOffset = 1f;

    private float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnPipe();
            timer = 0;
        }
    }

    private void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Vector3 randomPosition = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), transform.position.z);

        Instantiate(pipe, randomPosition, Quaternion.identity);
    }
}

    // Update is called once per frame
    
