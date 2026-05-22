using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statsMesh;
    [SerializeField] private GameObject speedUpArrowGameObject;
    [SerializeField] private GameObject speedDownArrowGameObject;
    [SerializeField] private GameObject speedLeftArrowGameObject;
    [SerializeField] private GameObject speedRightArrowGameObject;
    [SerializeField] private Image fuelImage;


    private void Awake()
    {

    }
    private void UpdateStatsMesh()
    {
        statsMesh.text = GameManager.Instance.GetLevelNumber() + "\n" +
                            GameManager.Instance.GetScore() + "\n" +
                            Mathf.Round(GameManager.Instance.GetTime()) + "\n" +
                            Mathf.Round(Lander.Instance.GetSpeedX() * 10f) + "\n" +
                            Mathf.Round(Lander.Instance.GetSpeedY() * 10f) + "\n";

        fuelImage.fillAmount = Lander.Instance.GetMaxFuelAmount;
    }
    private void GetSpeedDirection()
    {
        speedUpArrowGameObject.SetActive(Lander.Instance.GetSpeedY() >= 0);
        speedDownArrowGameObject.SetActive(Lander.Instance.GetSpeedY() < 0);
        speedLeftArrowGameObject.SetActive(Lander.Instance.GetSpeedX() < 0);
        speedRightArrowGameObject.SetActive(Lander.Instance.GetSpeedX() >= 0);
    }

    private void Update()
    {
        UpdateStatsMesh();
        GetSpeedDirection();
    }
}
