using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayClickSound()
    {
        audioSource.Play();
    }
}