using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Música")]
    public AudioSource musicSource;
    public AudioClip backgroundMusic;

    [Header("Efeitos Sonoros")]
    public AudioSource sfxSource;
    public AudioClip hitPaddleSound;
    public AudioClip hitWallSound;
    public AudioClip scoreSound;
    public AudioClip victorySound;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            // Tenta encontrar os AudioSources automaticamente se não foram atribuídos
            AudioSource[] sources = GetComponents<AudioSource>();
            if (musicSource == null && sources.Length > 0) musicSource = sources[0];
            if (sfxSource == null && sources.Length > 1) sfxSource = sources[1];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBackgroundMusic()
    {
        if (musicSource == null) { Debug.LogError("AudioManager: 'Music Source' (AudioSource) está faltando no objeto AudioManager!"); return; }
        
        if (backgroundMusic == null) 
        { 
            Debug.LogError("AudioManager: O arquivo de música de fundo não foi atribuído! Arraste o arquivo 'Alok _ Bhaskar - FUEGO' para o campo 'Background Music' no Inspector do AudioManager.");
            return; 
        }
        
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
        Debug.Log("AudioManager: Iniciando música de fundo: " + backgroundMusic.name);
    }

    public void StopBackgroundMusic()
    {
        if (musicSource != null) musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null) { Debug.LogError("AudioManager: 'Sfx Source' (AudioSource) está faltando!"); return; }
        if (clip == null) { Debug.LogWarning("AudioManager: Tentando tocar um som que não foi atribuído no Inspector!"); return; }
        
        sfxSource.PlayOneShot(clip);
    }

    public void PlayHitPaddle() => PlaySFX(hitPaddleSound);
    public void PlayHitWall() => PlaySFX(hitWallSound);
    public void PlayScore() => PlaySFX(scoreSound);
    public void PlayVictory() => PlaySFX(victorySound);
}
