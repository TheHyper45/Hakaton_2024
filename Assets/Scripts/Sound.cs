using UnityEngine;

public class Sound : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource startupAudioSource;
    [SerializeField] private AudioSource loopAudioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip startupSound;
    [SerializeField] private AudioClip loopSound;

    [Header("Settings")]
    [SerializeField] private float crossfadeTime = 0.1f; // Czas płynnego przejścia między utworami
    
    private float startTime;
    private bool isPreparingLoop = false;

    private void Awake()
    {
        // Create AudioSource components if they don't exist
        if (startupAudioSource == null)
        {
            startupAudioSource = gameObject.AddComponent<AudioSource>();
            startupAudioSource.playOnAwake = false;
        }

        if (loopAudioSource == null)
        {
            loopAudioSource = gameObject.AddComponent<AudioSource>();
            loopAudioSource.playOnAwake = false;
            loopAudioSource.loop = true;
            loopAudioSource.volume = 0f; // Startujemy z zerową głośnością dla płynnego przejścia
        }
    }

    private void Start()
    {
        if (startupSound != null)
        {
            // Przygotuj i odtwórz dźwięk startowy
            startupAudioSource.clip = startupSound;
            startupAudioSource.Play();
            startTime = Time.time;

            // Przygotuj dźwięk zapętlony
            if (loopSound != null)
            {
                loopAudioSource.clip = loopSound;
                loopAudioSource.Play(); // Rozpocznij odtwarzanie od razu
                loopAudioSource.volume = 0f; // Ale z zerową głośnością
                isPreparingLoop = true;
            }
        }
        else if (loopSound != null)
        {
            // Jeśli nie ma dźwięku startowego, odtwórz tylko pętlę
            loopAudioSource.clip = loopSound;
            loopAudioSource.volume = 1f;
            loopAudioSource.Play();
        }
    }

    private void Update()
    {
        if (isPreparingLoop && startupSound != null)
        {
            float timeSinceStart = Time.time - startTime;
            float timeToEnd = startupSound.length - timeSinceStart;

            // Rozpocznij crossfade tuż przed końcem pierwszego utworu
            if (timeToEnd <= crossfadeTime)
            {
                float t = 1f - (timeToEnd / crossfadeTime);
                startupAudioSource.volume = Mathf.Lerp(1f, 0f, t);
                loopAudioSource.volume = Mathf.Lerp(0f, 1f, t);

                if (timeToEnd <= 0f)
                {
                    // Zakończ crossfade
                    startupAudioSource.Stop();
                    loopAudioSource.volume = 1f;
                    isPreparingLoop = false;
                }
            }
        }
    }

    // Public methods to control the audio
    public void StopLoop()
    {
        loopAudioSource.Stop();
    }

    public void PauseLoop()
    {
        loopAudioSource.Pause();
    }

    public void ResumeLoop()
    {
        loopAudioSource.UnPause();
    }

    public void SetLoopVolume(float volume)
    {
        if (!isPreparingLoop) // Nie zmieniaj głośności podczas crossfade
        {
            loopAudioSource.volume = Mathf.Clamp01(volume);
        }
    }
}