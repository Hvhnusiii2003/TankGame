using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint; // Viết thường theo quy chuẩn

    [Header("Tank Settings")]
    public float bulletForce = 10f;
    public float recoilForce = 5f;
    public float dragFactor = 2f;

    private Rigidbody2D tankRb;
    public bool isAlive = true;
    public GameObject GameOver;



    void Start()
    {
        // Lấy thành phần Rigidbody2D khi khởi động
        tankRb = GetComponent<Rigidbody2D>();
        tankRb.linearDamping = dragFactor;
    }

    void Update()
    {
        if (isAlive)
        {
            // Quay xe tăng theo con trỏ chuột
            RotateTank();

            // Xử lý bắn đạn khi nhấn chuột trái
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }    
        
    }

    private void RotateTank()
    {
        // Lấy vị trí con trỏ chuột trong thế giới
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Đảm bảo tọa độ z bằng 0

        // Tính toán góc quay từ vị trí xe tăng đến con trỏ chuột
        Vector2 direction = (Vector2)(mousePosition - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Quay xe tăng
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Shoot()
    {
        
        // Tạo đạn
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Áp dụng lực cho đạn
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        // Áp dụng giật lùi cho xe tăng sau khi bắn
        recoilTank();
    }

    private void recoilTank()
    {
        // Áp dụng lực giật lùi lên xe tăng
        tankRb.AddForce(-firePoint.up * recoilForce, ForceMode2D.Impulse);
    }
    public void Die()
    {
        isAlive = false; // Đặt trạng thái người chơi là chết
        GameOver.SetActive(true); // Hiển thị màn hình Game Over
        Time.timeScale = 0; // Dừng thời gian game
    }
}
