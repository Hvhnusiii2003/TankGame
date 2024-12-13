using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Tank Settings")]
    public float bulletForce = 10f;
    public float recoilForce = 5f;
    public float dragFactor = 2f;

    private Rigidbody2D tankRb;
    public bool isAlive = true;
    public GameObject GameOver;
    public TextMeshProUGUI TxtScore;



    void Start()
    {
        tankRb = GetComponent<Rigidbody2D>();
        tankRb.linearDamping = dragFactor;
    }

    void Update()
    {
        if (isAlive)
        {
            RotateTank();

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }    
        
    }

    private void RotateTank()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (Vector2)(mousePosition - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Shoot()
    {
        
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        recoilTank();
    }

    private void recoilTank()
    {
        tankRb.AddForce(-firePoint.up * recoilForce, ForceMode2D.Impulse);
    }
    public void Die()
    {
        isAlive = false;
        GameOver.SetActive(true);
        TxtScore.text = "Score: " + GameManager.instance.score.ToString();
        Time.timeScale = 0;
    }
}
