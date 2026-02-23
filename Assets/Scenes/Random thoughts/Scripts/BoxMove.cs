using UnityEngine;

public class BoxMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Speed at which the box moves
    private int direction = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float getSpeed() => moveSpeed;
    public int EndDirection() => direction = 0;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime * Vector3.right);
    }
}
