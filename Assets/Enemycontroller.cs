using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f; // Adjust for how fast it approaches
    public int health = 3; // Starting health (set in Inspector)
    private Transform player; // Reference to player
    private Rigidbody2D rb; // For physics movement
    private Vector3 initialScale; // For flipping

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find player by tag
        initialScale = transform.localScale; // Store original scale for flipping
        if (player == null)
            Debug.LogWarning("Player not found! Tag the player as 'Player'.");
    }

    void Update()
    {
        if (player == null) return;

        float direction = player.position.x > transform.position.x ? 1f : -1f;

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        // Flip sprite to face the OPPOSITE direction of movement (backwards fix)
        if (direction > 0)
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z); // Face left when moving right
        else
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z); // Face right when moving left
    }

    // Method to handle damage from bullets
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
        Debug.Log("Enemy hit! Health left: " + health); // Optional debug
    }

    private void Die()
    {
        Destroy(gameObject); // Make the enemy disappear
        Debug.Log("Enemy destroyed!");
    }
}
