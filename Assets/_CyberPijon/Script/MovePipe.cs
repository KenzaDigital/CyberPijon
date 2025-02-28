using UnityEngine;

public class MovePipe : MonoBehaviour
{
    [SerializeField] 
    public float moveSpeed =5f; 
    public float deadZone= -10f;    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * (Time.deltaTime * moveSpeed);
        if(transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}
