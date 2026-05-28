using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI soundTextMesh;
    [SerializeField] private TextMeshProUGUI musicTextMesh;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.UnpauseGame();
        });
        quitButton.onClick.AddListener(() =>
        {
            GameManager.Instance.QuitGame();
        });

        soundButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeSoundVolume();
            soundTextMesh.text = "SOUND " + SoundManager.Instance.GetSoundVolumeNormalize() * 10;
        });

        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeMusicVolume();
            musicTextMesh.text = "MUSIC " + MusicManager.Instance.GetMusicVolumeNormalize() * 10;
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        soundTextMesh.text = "SOUND " + (SoundManager.Instance.GetSoundVolumeNormalize() * 10);
        musicTextMesh.text = "MUSIC " + (MusicManager.Instance.GetMusicVolumeNormalize() * 10);
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
