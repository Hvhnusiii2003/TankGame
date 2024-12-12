using UnityEngine;

public class BulletController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra nếu đạn va chạm với Enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Gọi hàm Die() từ Enemy để tiêu diệt kẻ địch
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Die(); // Gọi hàm Die() sẽ cộng điểm và hủy kẻ địch
            }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }    
        // Hủy đạn sau khi va chạm
        Destroy(gameObject);
    }

}
