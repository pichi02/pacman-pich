using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    [SerializeField] private GameObject[] hearts;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource pickPointsSound;
    [SerializeField] private AudioSource pickPillsSound;
    //private Movement movement;

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

    bool changingDirection;
    List<KeyCode> keys = new List<KeyCode>();
    bool canMove;
    float time;
    Vector2 position;

    bool removeKey = false;

    private void Awake()
    {
        //movement = GetComponent<Movement>();
        initialPosition = new Vector2(3f, 1f);
        Time.timeScale = 1;
    }
    private void Start()
    {
        position = transform.position;
    }

    void Update()
    {
        Move();
        UpdateScore();
        CheckGameOver();
        CheckWin();
        CheckDirection();

    }

    private void AddKey(KeyCode key)
    {
        if (keys.Count < 2)
        {
            if (keys.Count > 0)
            {
                if (keys[0] != key)
                {
                    keys.Add(key);
                }
            }
            else
            {
                keys.Add(key);
            }
        }
    }

    private void CheckDirection()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddKey(KeyCode.W);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            AddKey(KeyCode.A);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            AddKey(KeyCode.S);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AddKey(KeyCode.D);
        }
    }
    private void Move()
    {
        if (keys.Count > 0)
        {
            switch (keys[0])
            {
                case KeyCode.W:
                    MoveLerp(position, Vector3.up);
                    break;
                case KeyCode.A:
                    MoveLerp(position, Vector3.left);
                    break;
                case KeyCode.S:
                    MoveLerp(position, Vector3.down);
                    break;
                case KeyCode.D:
                    MoveLerp(position, Vector3.right);
                    break;
                default:
                    break;
            }


        }

        if (keys.Count == 2)
        {
            changingDirection = true;
        }
        //if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    movement.SetDirection(Vector2.up);
        //}
        //else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    movement.SetDirection(Vector2.down);
        //}
        //else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    movement.SetDirection(Vector2.left);
        //}
        //else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    movement.SetDirection(Vector2.right);
        //}

    }
    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            pickPointsSound.Play();
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
            pickPillsSound.Play();
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
            music.Stop();
        }
    }

    public void ResetPacman()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            keys.Remove(keys[i]);
        }
        transform.position = initialPosition;
        time = 0;
        canMove = false;
        position = transform.position;
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


    private void MoveLerp(Vector3 initPos, Vector3 movingVector)
    {
        if (MapCreator.GetTileTypeByPosition(Mathf.RoundToInt((transform.position.x + movingVector.x)), Mathf.RoundToInt((transform.position.y + movingVector.y))) != TileType.WALL && time == 0)
        {
            canMove = true;
        }
        if (canMove)
        {
            if (time <= 1)
            {
                transform.position = Vector3.Lerp(initPos, initPos + movingVector, time += Time.deltaTime * 5f);
                if (time >= 1)
                {
                    time = 1;
                    if (changingDirection)
                    {
                        MakeChangeDirection();
                    }
                    position = transform.position;
                    if (MapCreator.GetTileTypeByPosition((transform.position.x + movingVector.x), (transform.position.y + movingVector.y)) != TileType.WALL)
                    {
                        time = 0;
                    }
                    else
                    {
                        canMove = false;
                    }
                    time = 0;
                }
            }
        }
        else
        {
            time = 0;
        }

        if (changingDirection && time == 0)
        {
            MakeChangeDirection();
        }

    }
    private void MakeChangeDirection()
    {
        if (keys.Count == 2)
        {
            bool canChange = false;
            KeyCode lastKey = keys[0];

            switch (keys[1])
            {
                case KeyCode.W:
                    if (MapCreator.GetTileTypeByPosition((transform.position.x), (transform.position.y + Vector3.up.y)) != TileType.WALL)
                    {
                        canChange = true;
                    }
                    break;
                case KeyCode.S:
                    if (MapCreator.GetTileTypeByPosition((transform.position.x), (transform.position.y - Vector3.up.y)) != TileType.WALL)
                    {
                        canChange = true;
                    }
                    break;
                case KeyCode.D:
                    if (MapCreator.GetTileTypeByPosition((transform.position.x + Vector3.right.x), (transform.position.y)) != TileType.WALL)
                    {
                        canChange = true;
                    }
                    break;
                case KeyCode.A:
                    if (MapCreator.GetTileTypeByPosition((transform.position.x - Vector3.right.x), (transform.position.y)) != TileType.WALL)
                    {
                        canChange = true;
                    }
                    break;
            }
            if (canChange)
            {
                CheckChangeDirection();
            }
            else
            {
                canChange = false;
                removeKey = true;
                keys.Remove(keys[1]);
            }
        }
    }

    private void CheckChangeDirection()
    {
        changingDirection = false;
        keys[0] = keys[1];
        keys.Remove(keys[1]);
        canMove = false;
        time = 0;
    }

    public void PortalColiision()
    {
        time = 0;
        //for (int i = 0; i < keys.Count; i++)
        //{
        //    keys.Remove(keys[i]);
        //}
        position = transform.position;
    }

}
