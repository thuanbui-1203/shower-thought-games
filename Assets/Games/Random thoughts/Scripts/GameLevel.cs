using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform landerStartPositionTransform;

    public static GameLevel Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public Vector3 GetLanderStartPosition()
    {
        return landerStartPositionTransform.position;
    }
}
