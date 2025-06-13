using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    public int damage = 20; // ���� ������

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("���� ��Ʈ!");
        }
    }
}
