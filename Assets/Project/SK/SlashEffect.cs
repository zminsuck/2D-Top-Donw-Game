using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    public int damage = 20; // 참격 데미지
    public AudioClip slashSound; // 사운드 클립 지정
    private AudioSource audioSource;

    void Start()
    {
         audioSource = GetComponent<AudioSource>();

        // 사운드가 있고, AudioSource가 존재하면 사운드 재생
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
            Debug.Log("참격 히트!");
        }
    }
}
