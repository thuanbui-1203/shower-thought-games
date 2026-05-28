using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private const int MAX_MUSIC_VOLUME = 10;
    private int musicVolume = 5;
    private static float musicTime;
    private AudioSource musicAudioSource;
    public event EventHandler OnMusicVolumeChanged;

    private void Awake()
    {
        Instance = this;
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.time = musicTime;
    }
    private void Start()
    {
        musicAudioSource.volume = GetMusicVolumeNormalize();
    }
    private void Update()
    {
        musicTime = musicAudioSource.time;
    }

    public float GetMusicVolumeNormalize()
    {
        return (float)musicVolume / MAX_MUSIC_VOLUME;
    }
    public void ChangeMusicVolume()
    {
        OnMusicVolumeChanged?.Invoke(this, EventArgs.Empty);
        musicVolume = (musicVolume + 1) % MAX_MUSIC_VOLUME;
        musicAudioSource.volume = GetMusicVolumeNormalize();
    }
}
