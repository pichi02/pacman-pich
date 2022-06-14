using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;
    private Movement movement;

    public delegate void PillEaten();
    public event PillEaten OnPillEat;

    public delegate void GameOver();
    public GameOver OnGameOver;

    public delegate void Win();
    public event Win OnWin;

    private Vector2 initialPosition;

    private int score = 0;
    private int lives = 1;

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
        UpdateLives();
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

        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    private void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
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
        Debug.Log(lives);
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
