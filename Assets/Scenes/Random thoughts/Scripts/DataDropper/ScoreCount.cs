using TMPro;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject[] _elements;
    private int _score;
    private void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // private void UpdateScore()
    // {
    //     foreach (GameObject e in _elements)
    //     {
    //         if (!e.activeSelf)
    //         {
    //             _score++;
    //         }
    //     }
    //     _scoreText.text = $"Score: {_score}";
    //     return;
    // }
}
