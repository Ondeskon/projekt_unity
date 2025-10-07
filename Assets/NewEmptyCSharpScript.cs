using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public GameObject bulletPrefab;
    public Transform gunTip;
    public float fireRate = 0.5f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float nextFireTime;
    private Vector3 initialScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;
        nextFireTime = 0;
        Debug.Log("Player initialized, gunTip: " + (gunTip != null) + ", bulletPrefab: " + (bulletPrefab != null) + " at " + Time.time + " CEST");
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput > 0)
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        if (Input.GetMouseButton(0) && Time.time > nextFireTime)
        {
            Debug.Log("Mouse clicked at " + Time.time + " CEST, nextFireTime: " + nextFireTime);
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Debug.Log("Shoot called, gunTip: " + (gunTip != null) + ", bulletPrefab: " + (bulletPrefab != null) + " at " + Time.time + " CEST");
        if (gunTip != null && bulletPrefab != null)
        {
            Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            GameObject bullet = Instantiate(bulletPrefab, gunTip.position, Quaternion.identity);
            bullet.GetComponent<BulletController>().direction = direction;
            Debug.Log("Bullet fired at " + gunTip.position + " at " + Time.time + " CEST");
        }
        else
        {
            Debug.LogWarning("Cannot shoot: gunTip or bulletPrefab is null at " + Time.time + " CEST");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}