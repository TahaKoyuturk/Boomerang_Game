using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource CoinAudioSource;
    [SerializeField] private AudioSource BummerangAudioSource;
    public static AudioController Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayCoinSound()
    {
        CoinAudioSource.Play();
    }
    public void PlayBummerangSound()
    {
        BummerangAudioSource.Play();
    }

}
