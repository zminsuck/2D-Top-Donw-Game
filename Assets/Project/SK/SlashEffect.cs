using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    public int damage = 20; // 참격 데미지

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("참격 히트!");
        }
    }
}
