using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;  // “˝”√ AudioSource ◊Èº˛

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

      //  PlayMusic();
    }

    // ≤•∑≈“Ù¿÷
    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // ‘›Õ£“Ù¿÷
    public void PauseMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    // Õ£÷π“Ù¿÷
    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    // …Ë÷√“Ù¡ø
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    // «–ªª≤•∑≈/‘›Õ£◊¥Ã¨
    public void TogglePlayPause()
    {
        if (audioSource != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
            else
            {
                audioSource.Play();
            }
        }
    }
}
