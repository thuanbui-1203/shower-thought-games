using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Transform landerStartPositionTransform;
    [SerializeField] private Transform cameraStartPositionTransform;
    [SerializeField] private float zoomedOutOrthographicSize;

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

    public Transform GetCameraStartPositionTransform()
    {
        return cameraStartPositionTransform;
    }

    public float GetZoomedOutOrthographicSize()
    {
        return zoomedOutOrthographicSize;
    }
}
