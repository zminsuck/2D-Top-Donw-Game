using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    public int damage = 20; // ���� ������
    public AudioClip slashSound; // ���� Ŭ�� ����
    private AudioSource audioSource;

    void Start()
    {
         audioSource = GetComponent<AudioSource>();

        // ���尡 �ְ�, AudioSource�� �����ϸ� ���� ���
        if (slashSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(slashSound);
        }

        Destroy(gameObject, 1.5f);
    }
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
