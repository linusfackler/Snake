using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    private int horizontal;
    private int vertical;


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    public void Move(InputAction.CallbackContext context)
    {        
        horizontal = (int)context.ReadValue<Vector2>().x;
        vertical = (int)context.ReadValue<Vector2>().y;
        print("horizontal: " + horizontal);
        print("vertical: " + vertical);
    }
}
