using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip fuelPickupAudioClip;
    [SerializeField] private AudioClip coinPickupAudioClip;
    [SerializeField] private AudioClip crashAudioClip;
    [SerializeField] private AudioClip landingSuccessAudioClip;
    public static int soundVolume = 5;
    private const int MAX_SOUND_VOLUME = 10;
    public static SoundManager Instance { get; private set; }
    public event EventHandler OnSoundVolumeChanged;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnFuelPickup += Lander_OnFuelPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        switch (e.landingType)
        {
            case Lander.LandingType.Success:
                AudioSource.PlayClipAtPoint(landingSuccessAudioClip, Camera.main.transform.position, GetSoundVolumeNormalize());
                break;
            default:
                AudioSource.PlayClipAtPoint(crashAudioClip, Camera.main.transform.position, GetSoundVolumeNormalize());
                break;
        }
    }

    private void Lander_OnFuelPickup(object sender, EventArgs e)
    {
        AudioSource.PlayClipAtPoint(fuelPickupAudioClip, Camera.main.transform.position, GetSoundVolumeNormalize());
    }

    private void Lander_OnCoinPickup(object sender, EventArgs e)
    {
        AudioSource.PlayClipAtPoint(coinPickupAudioClip, Camera.main.transform.position, GetSoundVolumeNormalize());
    }

    public float GetSoundVolumeNormalize()
    {
        return (float)soundVolume / MAX_SOUND_VOLUME;
    }

    public void ChangeSoundVolume()
    {
        soundVolume = (soundVolume + 1) % MAX_SOUND_VOLUME;
        OnSoundVolumeChanged?.Invoke(this, EventArgs.Empty);
    }
}
