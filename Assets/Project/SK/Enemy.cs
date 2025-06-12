using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 1.2f;
    public LayerMask playerLayer;
    public Transform attackPoint;

    private Animator anim;
    private Rigidbody2D rb;
    private Transform player;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("Walk", false);
            anim.SetTrigger("Attack");
        }
        else if (distance <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

            anim.SetBool("Walk", true);
            Flip(direction.x);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("Walk", false);
        }
    }

    void Flip(float dirX)
    {
        if (dirX == 0) return;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (dirX > 0 ? 1 : -1);
        transform.localScale = scale;
    }

    public void TakeDamage()
    {
        if (isDead) return;

        anim.SetTrigger("Hurt");
        isDead = true;

        anim.SetTrigger("Dead");
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
    }

    public void Attack()
    {
        if (isDead) return;
        anim.SetTrigger("Attack");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Player"))
        {
            Attack();
        }
    }

    void PerformAttack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            Debug.Log("플레이어 적중!");
        }
    }

}
