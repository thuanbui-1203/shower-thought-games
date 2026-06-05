using Unity.Cinemachine;
using UnityEngine;

public class CinemachineCameraZoom2D : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static CinemachineCameraZoom2D Instance { get; private set; }
    private float targetOrthographicSize = 10f;
    private const float NORMAL_ORTHOGRAPHIC_SIZE = 10f;
    [SerializeField] private CinemachineCamera cinemachineCamera;

    private void Awake()
    {
        Instance = this;
    }

    public void SetTargetOrthographicSize(float targetOrthographicSize)
    {
        this.targetOrthographicSize = targetOrthographicSize;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cinemachineCamera.Lens.OrthographicSize = Mathf.Lerp(cinemachineCamera.Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * 2f);
    }

    public void SetNormalOrthographicSize()
    {
        SetTargetOrthographicSize(NORMAL_ORTHOGRAPHIC_SIZE);
    }
}
