using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip backgroundMusic;
    private AudioSource audioSource;

    void Awake()
    {
        // ½Ì±ÛÅæ À¯Áö
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.volume = 0.5f;

            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
