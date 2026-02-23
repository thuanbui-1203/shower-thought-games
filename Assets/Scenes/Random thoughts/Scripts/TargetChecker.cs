using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetChecker : MonoBehaviour
{
    [SerializeField] private TMP_Text _targetText;
    [SerializeField] private GameObject _movingBox;
    private BoxMove _boxMoveScript;
    private float _timeLimit = 5f; // Time limit in seconds
    private float _timer = 0f;
    private float distance;
    private float playerSpeed;

    private void Awake()
    {
        _boxMoveScript = _movingBox.GetComponent<BoxMove>();
        playerSpeed = _boxMoveScript.getSpeed();
    }
    public void StopBox()
    {
        _boxMoveScript.EndDirection();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player reached the target! Time taken: " + _timer.ToString("F2") + " seconds");
            // You can add additional logic here, such as progressing to the next level or showing a success message
            Time.timeScale = 0f; // Pause the game
        }
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, _movingBox.transform.position);
        _timer += Time.deltaTime;
        if (_timer >= _timeLimit)
        {
            Debug.Log("Time's up! Player failed to reach the target.");
            // You can add additional logic here, such as resetting the game or showing a failure message
            Time.timeScale = 0f; // Pause the game
        }

        _targetText.text = "\nPlayer speed: " + playerSpeed + " \nDistance to target: " + distance.ToString("F2") + " \nTime left: " + (_timeLimit - _timer).ToString("F2") + " seconds";
    }
}
