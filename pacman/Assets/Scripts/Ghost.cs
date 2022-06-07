using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Movement movement;
    private void Awake()
    {
        movement = GetComponent<Movement>();

    }
    private void Start()
    {
        movement.SetDirection(Vector2.right);
    }
    void Update()
    {

        int rand = Random.Range(0, 4);

        Debug.Log(movement.GetDirection());
        if (movement.GetDirection() == Vector2.up)
        {
            Debug.Log("entra");
            rand = Random.RandomRange(0, 3);
            if (rand == 0)
            {
                movement.SetDirection(Vector2.right);
            }
            else if (rand == 1)
            {
                movement.SetDirection(Vector2.left);
            }
            else
            {
                movement.SetDirection(Vector2.up);
            }
        }
        else if (movement.GetDirection() == Vector2.down)
        {
            Debug.Log("entra");
            rand = Random.RandomRange(0, 3);
            if (rand == 0)
            {
                movement.SetDirection(Vector2.right);
            }
            else if (rand == 1)
            {
                movement.SetDirection(Vector2.left);
            }
            else
            {
                movement.SetDirection(Vector2.down);
            }
        }
        else if (movement.GetDirection() == Vector2.right)
        {
            Debug.Log("entra");
            rand = Random.RandomRange(0, 3);
            if (rand == 0)
            {
                movement.SetDirection(Vector2.right);
            }
            else if (rand == 1)
            {
                movement.SetDirection(Vector2.up);
            }
            else
            {
                movement.SetDirection(Vector2.down);
            }
        }
        else if (movement.GetDirection() == Vector2.left)
        {
            Debug.Log("entra");
            rand = Random.RandomRange(0, 3);
            if (rand == 0)
            {
                movement.SetDirection(Vector2.left);
            }
            else if (rand == 1)
            {
                movement.SetDirection(Vector2.up);
            }
            else
            {
                movement.SetDirection(Vector2.down);
            }
        }

    }
}
