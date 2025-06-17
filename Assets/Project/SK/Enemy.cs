using UnityEngine;
using UnityEngine.Audio;

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

    public AudioClip attackSound;
    private AudioSource audioSource;

    private int currentHealth;
    private float lastAttackTime;
    private Animator animator;
    private PlayerHealth playerHealth;

    [Header("HP Bar ฐüทร")]
    public Transform hpAnchor;
    public GameObject hpBarPrefab;
    private EnemyHPBar hpBar;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerHealth = player.GetComponent<PlayerHealth>();

        if (hpBarPrefab != null && hpAnchor != null)
        {
            GameObject hpGO = Instantiate(hpBarPrefab, FindFirstObjectByType<Canvas>().transform);
            hpBar = hpGO.GetComponent<EnemyHPBar>();
            hpBar.Initialize(hpAnchor);
            hpBar.SetHP(currentHealth, maxHealth);
        }
    }

    void Update()
    {
        if (player == null || currentHealth <= 0 || (playerHealth != null && playerHealth.IsDead))
        {
            animator.SetBool("Walk", false);
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

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

    public void DealDamageToPlayer()
    {
        if (playerHealth != null && !playerHealth.IsDead &&
            Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            playerHealth.TakeDamage(damage);
            if (attackSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(attackSound);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        animator.SetTrigger("Hurt");

        if (hpBar != null)
            hpBar.SetHP(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Dead");
        OnDeath?.Invoke();

        GameManager.Instance.AddScore(100);
        if (hpBar != null)
            Destroy(hpBar.gameObject);

        Destroy(gameObject, 2f);
    }
}
