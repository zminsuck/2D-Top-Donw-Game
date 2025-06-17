using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public Image fillImage;
    private Transform anchor;

    public void Initialize(Transform anchorTransform)
    {
        anchor = anchorTransform;
    }

    public void SetHP(float current, float max)
    {
        fillImage.fillAmount = current / max;
    }

    private void LateUpdate()
    {
        if (anchor == null) return;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(anchor.position);
        transform.position = screenPosition;
    }
}
