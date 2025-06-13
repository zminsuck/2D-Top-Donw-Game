using UnityEngine;
using UnityEngine.UI;
using SmallScaleInc.TopDownPixelCharactersPack1;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private AnimationController animationController;

    [Header("UI")]
    public Image hpBarFill;
    public float smoothSpeed = 5f;

    private float targetFill = 1f;

    public bool IsDead { get; private set; } = false; // 플레이어 사망 여부 확인용

    void Start()
    {
        currentHealth = maxHealth;
        animationController = GetComponent<AnimationController>();
        targetFill = 1f;
        UpdateHPBarInstant();
    }

    void Update()
    {
        if (hpBarFill != null)
        {
            hpBarFill.fillAmount = Mathf.Lerp(hpBarFill.fillAmount, targetFill, Time.deltaTime * smoothSpeed);
        }
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        targetFill = (float)currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            IsDead = true;
            animationController.TriggerDie();
            Die();
        }
        else
        {
            animationController.TriggerTakeDamageAnimation();
        }
    }

    void UpdateHPBarInstant()
    {
        if (hpBarFill != null)
            hpBarFill.fillAmount = (float)currentHealth / maxHealth;
    }

    void Die()
    {
        // 사망 애니메이션 등 추가 가능
        FindFirstObjectByType<GameManager>().GameOver();
        gameObject.SetActive(false); // 플레이어 비활성화
    }
}
