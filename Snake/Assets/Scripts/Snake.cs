using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    private float horizontal;
    private float vertical;


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }
}
