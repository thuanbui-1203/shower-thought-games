using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableItem : MonoBehaviour
{
    public DataType currentDataType;
    [SerializeField] private GameObject _dataBox;
    [SerializeField] private string _dataTextString;
    [SerializeField] private TMP_Text _dataTypeText;
    private Vector3 _currentPosition;
    private Vector3 mousePos;
    private SpriteRenderer _spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _spriteRenderer = _dataBox.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        _currentPosition = transform.position;
        transform.position = RandomItemsLocation.GenerateRandomLocation();
        _dataTypeText.text = _dataTextString;
        switch (currentDataType)
        {
            case DataType.String:
                _dataTypeText.text = $"\"{_dataTextString}\"";
                _spriteRenderer.color = Color.orange;
                break;
            case DataType.Int:
                _spriteRenderer.color = Color.cyan;
                break;
            case DataType.Float:
                _spriteRenderer.color = Color.green;
                break;
            case DataType.Bool:
                _spriteRenderer.color = Color.magenta;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Ensure the z-coordinate is set to 0
        transform.position = mousePos;
    }

    void OnMouseUp()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
        foreach (Collider2D coll in colliders)
        {
            DropZone dropZone = coll.GetComponent<DropZone>();
            if (dropZone != null)
            {
                // Debug.Log(dropZone.currentDataType);
                if (dropZone.currentDataType.Equals(currentDataType))
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    transform.position = _currentPosition;
                }
            }
        }
    }
}
