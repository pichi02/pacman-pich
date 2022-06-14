using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Text scoreText;
    private Movement movement;

    public delegate void PillEaten();
    public event PillEaten OnPillEat;


    private int score = 0;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        Move();
        UpdateScore();
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            Destroy(collision.gameObject);
            score += 10;
        }
        else if (collision.gameObject.CompareTag("Pill"))
        {
            OnPillEat?.Invoke();
            Destroy(collision.gameObject);
        }



    }


}
