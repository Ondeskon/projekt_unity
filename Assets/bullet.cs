using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction = Vector2.right; // Default to right

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Reduce health by 1 per hit
            }
            Destroy(gameObject); // Destroy the bullet after hit
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // Remove bullet when off-screen
    }
}