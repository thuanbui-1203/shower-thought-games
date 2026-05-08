using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statsMesh;
    private void UpdateStatsMesh()
    {
        statsMesh.text = GameManager.Instance.GetScore() + "\n" +
                            Mathf.Round(GameManager.Instance.GetTime()) + "\n" +
                            Mathf.Round(Lander.Instance.GetSpeedX() * 10f) + "\n" +
                            Mathf.Round(Lander.Instance.GetSpeedY() * 10f) + "\n" +
                            Lander.Instance.GetFuelAmount();
    }

    private void Update()
    {
        UpdateStatsMesh();
    }
}
