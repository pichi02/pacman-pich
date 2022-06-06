using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Text scoreText;
    public Movement movement { get; private set; }

    private bool movingUp = false;
    private bool movingDown = false;
    private bool movingRight = false;
    private bool movingLeft = false;

    private int score = 0;

    private void Awake()
    {
      
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        Move();
        UpdateScore();
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z));
    }


    private void Move()
    {
      
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.SetDirection(Vector2.right);
        }


    }
    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            Destroy(collision.gameObject);
            score += 10;
        }
        else if (collision.gameObject.CompareTag("Pill"))
        {
            Destroy(collision.gameObject);
        }


    }
 

}
