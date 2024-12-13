using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int points = 10;
    public float moveSpeed = 3f;
    private Transform player;

    AudioController audioController;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }
    void Start()
    {
        
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        
        MoveTowardsPlayer();
    }   

    void MoveTowardsPlayer()
    {
        if (player != null)
        {           
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    public void Die()
    {
        audioController.PlaySFX(audioController.getpoint);
        GameManager.instance.AddScore(points);
        Destroy(gameObject);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            
            if (playerController != null)
            {
                playerController.Die();
            }
            Destroy(gameObject);

        }
    }
}
