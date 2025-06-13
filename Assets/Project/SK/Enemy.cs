using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float attackRange = 1f;
    public float attackCooldown = 1.5f;
    public int damage = 10;
    public int maxHealth = 100;
    public System.Action OnDeath;
    public LayerMask obstacleLayer;

    private int currentHealth;
    private float lastAttackTime;
    private Animator animator;
    private PlayerHealth playerHealth;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (player == null || currentHealth <= 0) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        // Raycast로 장애물 감지
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, obstacleLayer);

        bool isBlocked = hit.collider != null;

        if (!isBlocked && distance > attackRange)
        {
            animator.SetBool("Walk", true);
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            animator.SetBool("Walk", false);

            if (distance <= attackRange && Time.time > lastAttackTime + attackCooldown)
            {
                animator.SetTrigger("Attack");
                lastAttackTime = Time.time;
            }
        }
    }

    // 애니메이션 이벤트에서 호출
    public void DealDamageToPlayer()
    {
        if (playerHealth != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Dead");
        OnDeath?.Invoke();
        Destroy(gameObject, 2f);
    }
}
