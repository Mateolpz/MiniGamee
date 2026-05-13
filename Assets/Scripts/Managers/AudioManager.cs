using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Música por fase")]
    public AudioClip phase1Music; // lo-fi tranquilo
    public AudioClip phase2Music; // un poco más brillante
    public AudioClip phase3Music; // más alegre/luminoso

    [Header("Efectos de sonido")]
    public AudioClip orbCollectSound;
    public AudioClip obstacleHitSound;
    public AudioClip calmReachedSound;

    [Header("Configuración")]
    [Range(0f, 1f)] public float musicVolume = 0.4f;
    [Range(0f, 1f)] public float sfxVolume = 0.7f;
    public float fadeDuration = 1.5f;

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private int currentPhase = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Crear dos AudioSources: uno para música, uno para SFX
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = musicVolume;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.volume = sfxVolume;
    }

    void Start()
    {
        ChangePhase(1); // arranca con la fase 1
    }

    // --- CAMBIO DE FASE ---
    public void ChangePhase(int phase)
    {
        if (phase == currentPhase) return;
        currentPhase = phase;

        AudioClip newClip = phase switch
        {
            1 => phase1Music,
            2 => phase2Music,
            3 => phase3Music,
            _ => phase1Music
        };

        if (newClip != null)
            StartCoroutine(FadeToNewMusic(newClip));
    }

    // Fade suave entre músicas
    System.Collections.IEnumerator FadeToNewMusic(AudioClip newClip)
    {
        // Fade out
        float startVolume = musicSource.volume;
        float elapsed = 0f;

        while (elapsed < fadeDuration / 2f)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / (fadeDuration / 2f));
            yield return null;
        }

        // Cambiar clip
        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();

        // Fade in
        elapsed = 0f;
        while (elapsed < fadeDuration / 2f)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, musicVolume, elapsed / (fadeDuration / 2f));
            yield return null;
        }

        musicSource.volume = musicVolume;
    }

    // --- EFECTOS DE SONIDO ---
    public void PlayOrbCollect()
    {
        if (orbCollectSound != null)
            sfxSource.PlayOneShot(orbCollectSound, sfxVolume);
    }

    public void PlayObstacleHit()
    {
        if (obstacleHitSound != null)
            sfxSource.PlayOneShot(obstacleHitSound, sfxVolume);
    }

    public void PlayCalmReached()
    {
        if (calmReachedSound != null)
            sfxSource.PlayOneShot(calmReachedSound, sfxVolume);
    }

    // --- VOLUMEN ---
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        sfxSource.volume = volume;
    }
}