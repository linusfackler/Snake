using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Snake : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    private int horizontal;
    private int vertical;
    private int tempH;
    private int tempV;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 4;

    public GameObject scoreText;
    private int score = 0;
    public GameObject highscoreText;


    private void Start()
    {
        ResetState();
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
            _segments[i].rotation = _segments[i - 1].rotation;
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
            tempH = (int)context.ReadValue<Vector2>().x; 
            tempV = (int)context.ReadValue<Vector2>().y;
            if (tempH == 1 && horizontal == -1 || tempV == 1 && vertical == -1 || tempH == -1 && horizontal == 1 || tempV == -1 && vertical == 1 || tempV == 0 && tempH == 0)
                return;
            
            horizontal = tempH;
            vertical = tempV;

            if (horizontal > 0f || horizontal < 0f)
            {
                this.transform.eulerAngles = new Vector3(0f, 0f, 90f);
            }
            else
            {
                this.transform.eulerAngles = (new Vector3(0f, 0f, 0f));
            }
        }
        
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
        score += 10;
        scoreText.GetComponent<TMP_Text>().text = score.ToString("000000");
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;

        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highscoreText.GetComponent<TMP_Text>().text = score.ToString("000000");
        }
        scoreText.GetComponent<TMP_Text>().text = "000000";
        score = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
            Grow();
        
        else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }
}
