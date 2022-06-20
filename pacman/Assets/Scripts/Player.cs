using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Text scoreText;

    [SerializeField] private GameObject[] hearts;
    private Movement movement;

    public delegate void PillEaten();
    public event PillEaten OnPillEat;

    public delegate void GameOver();
    public GameOver OnGameOver;

    public delegate void Win();
    public event Win OnWin;

    private Vector2 initialPosition;

    private int score = 0;
    private int lives = 3;

    private bool allPointsCollected = false;
    private bool allPillsCollected = false;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        initialPosition = transform.position;
        Time.timeScale = 1;
    }

    void Update()
    {
        Move();
        UpdateScore();
        CheckGameOver();
        CheckWin();

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
            Point point = collision.gameObject.GetComponent<Point>();
            if (point.GetInstantiatedPoints() == 1)
            {
                allPointsCollected = true;
            }
            Destroy(collision.gameObject);
            score += 10;

        }
        else if (collision.gameObject.CompareTag("Pill"))
        {
            Pill pill = collision.gameObject.GetComponent<Pill>();
            if (pill.GetInstantiatedPills() == 1)
            {
                allPillsCollected = true;
            }
            OnPillEat?.Invoke();
            Destroy(collision.gameObject);
        }



    }
    public void IncreaseScore()
    {
        score += 50;
    }
    public void SubstractLive()
    {
        lives--;
        if (lives == 2)
        {
            Destroy(hearts[2]);
        }
        else if (lives == 1)
        {
            Destroy(hearts[1]);
        }
        else if (lives == 0)
        {
            Destroy(hearts[0]);
        }
    }

    public void ResetPacman()
    {
        transform.position = initialPosition;
    }
    public void CheckGameOver()
    {
        if (lives <= 0)
        {
            OnGameOver?.Invoke();
            Time.timeScale = 0;
        }
    }
    public void CheckWin()
    {
        if (allPillsCollected && allPointsCollected)
        {
            OnWin?.Invoke();
            Time.timeScale = 0;
        }
    }


}
