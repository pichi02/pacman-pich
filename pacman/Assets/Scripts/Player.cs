using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Text scoreText;

    Rigidbody2D rb;
    private bool movingUp = false;
    private bool movingDown = false;
    private bool movingRight = false;
    private bool movingLeft = false;

    private int score = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdateScore();
        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z));
    }


    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || movingUp)
        {
            rb.MovePosition(transform.position + Vector3.up * speed);
            movingUp = true;
            movingDown = false;
            movingRight = false;
            movingLeft = false;

        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || movingDown)
        {
            rb.MovePosition(transform.position + Vector3.down * speed);
            movingDown = true;
            movingUp = false;
            movingRight = false;
            movingLeft = false;

        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || movingLeft)
        {
            rb.MovePosition(transform.position + Vector3.left * speed);
            movingLeft = true;
            movingDown = false;
            movingRight = false;
            movingUp = false;

        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || movingRight)
        {
            RaycastHit2D collisionInfo = Physics2D.BoxCast(transform.position, new Vector2(1, 1), 0, new Vector2(0f, 0f));

            if (collisionInfo.transform.gameObject.CompareTag("Brick"))
            {
                //Debug.Log("pared");
            }


            rb.MovePosition(transform.position + Vector3.right * speed);
            movingRight = true;
            movingDown = false;
            movingUp = false;
            movingLeft = false;

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
    private void OnDrawGizmos()
    {

    }

}
