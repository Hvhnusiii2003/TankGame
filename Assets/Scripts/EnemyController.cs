using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int points = 10; // Số điểm khi tiêu diệt kẻ địch
    public float moveSpeed = 3f; // Tốc độ di chuyển của kẻ địch
    private Transform player; // Tham chiếu tới người chơi

    AudioController audioController;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }
    void Start()
    {
        // Tìm đối tượng Player trong cảnh
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        // Di chuyển kẻ địch về phía người chơi
        MoveTowardsPlayer();
    }   

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Tính toán hướng di chuyển (vector chỉ hướng từ kẻ địch đến người chơi)
            Vector2 direction = (player.position - transform.position).normalized;

            // Di chuyển kẻ địch về phía người chơi
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    public void Die()
    {
        audioController.PlaySFX(audioController.getpoint);
        GameManager.instance.AddScore(points);
        Destroy(gameObject);
        Debug.Log("Point:" +  points);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            
            if (playerController != null)
            {
                playerController.Die(); // Chuyển trạng thái người chơi thành "die"
            }

            // Hủy đối tượng kẻ địch
            Destroy(gameObject);

        }
    }
}
