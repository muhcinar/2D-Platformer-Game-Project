using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip fireball;
    public AudioClip sword;
    public AudioClip dash;
    public AudioClip enemyDamage;
    public AudioClip jump;
    public AudioClip heal;

    public AudioClip gameTheme;

    private void Start()
    {
        musicSource.clip = gameTheme;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip sfx)
    {
        Debug.Log("Playing sound");
        sfxSource.PlayOneShot(sfx);
    }
}
