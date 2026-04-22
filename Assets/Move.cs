using UnityEngine;
using UnityEngine.InputSystem;
public class Move : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            transform.Translate(Vector2.left * Time.deltaTime);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.Translate(Vector2.right * Time.deltaTime);
        }
        if (Keyboard.current.upArrowKey.isPressed)
        {
            transform.Translate(Vector2.up * Time.deltaTime);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            transform.Translate(Vector2.down * Time.deltaTime);
        }
    }
}
