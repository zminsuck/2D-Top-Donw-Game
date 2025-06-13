// 예시: FootstepSound.cs
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip[] footstepClips;
    public float footstepInterval = 0.5f;
    public AudioSource audioSource;

    private float timer;

    void Update()
    {
        if (IsWalking()) // 이동 중일 때
        {
            timer += Time.deltaTime;
            if (timer >= footstepInterval)
            {
                PlayFootstep();
                timer = 0f;
            }
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            int index = Random.Range(0, footstepClips.Length);
            audioSource.PlayOneShot(footstepClips[index]);
        }
    }

    bool IsWalking()
    {
        // 실제 움직임 체크 로직이 필요합니다.
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
    }
}
