using UnityEngine;

public class BulletController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Die(); 
            }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }    
        
        Destroy(gameObject);
    }

}
