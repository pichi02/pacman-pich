using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    Rigidbody2D rb;
    bool movingUp = false;
    bool movingDown = false;
    bool movingRight = false;
    bool movingLeft = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Move()
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
            rb.MovePosition(transform.position + Vector3.right * speed);
            movingRight = true;
            movingDown = false;
            movingUp = false;
            movingLeft = false;
        }
    }
}
