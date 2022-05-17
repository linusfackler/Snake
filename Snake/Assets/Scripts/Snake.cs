using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    private int horizontal;
    private int vertical;
    private List<Transform> _segments;
    public Transform segmentPrefab;

    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + horizontal,
            Mathf.Round(this.transform.position.y) + vertical,
            0.0f
        );
    }

    public void Move(InputAction.CallbackContext context)
    {        
        if (context.performed)
        {
            horizontal = (int)context.ReadValue<Vector2>().x;
            vertical = (int)context.ReadValue<Vector2>().y;
            print("horizontal: " + horizontal);
            print("vertical: " + vertical);
        }
        
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        this.transform.position = Vector3.zero;
        // SCORE
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
            Grow();
        
        else if (other.tag == "Obstacle")
            ResetState();
    }
}
