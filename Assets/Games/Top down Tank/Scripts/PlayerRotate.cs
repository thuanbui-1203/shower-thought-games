using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private float angle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        angle = Mathf.Atan2(worldMousePosition.y - transform.position.y, worldMousePosition.x - transform.position.x);
        transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90);
    }
}
