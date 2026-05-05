using TMPro;
using UnityEngine;

[RequireComponent(typeof(LandingPad))]
public class LandingPadVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshPro _scoreTextMeshPro;
    private void Awake()
    {
        LandingPad landingPad = GetComponent<LandingPad>();
        _scoreTextMeshPro.text = "X" + landingPad.GetScoreMultiplier;
    }
}
