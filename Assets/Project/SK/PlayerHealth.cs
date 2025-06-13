using UnityEngine;
using SmallScaleInc.TopDownPixelCharactersPack1;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private AnimationController animationController;

    void Start()
    {
        currentHealth = maxHealth;
        animationController = GetComponent<AnimationController>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            animationController.TriggerDie();
        }
        else
        {
            animationController.TriggerTakeDamageAnimation();
        }
    }
}
