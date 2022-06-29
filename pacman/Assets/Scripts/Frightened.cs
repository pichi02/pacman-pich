using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frightened : GhostModes
{

    public delegate void GhostEaten();
    public GhostEaten OnGhostEat;
    public bool eaten { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);


        Invoke(nameof(Flash), duration / 2f);
    }

    public override void Disable()
    {
        base.Disable();


    }

    private void Eaten()
    {
        eaten = true;
        ghost.SetPosition(ghost.home.GetInside().position);
        ghost.frightened.Disable();
        ghost.home.Enable(duration);

    }

    private void Flash()
    {
        if (!eaten)
        {

        }
    }

    private void OnEnable()
    {
        ghost.movement.ChangeSpeedMultiplier(1f);
        eaten = false;
        ghost.ChangeColor(Color.blue);
        ghost.chase.Disable();
        ghost.scatter.Disable();
    }

    private void OnDisable()
    {
        ghost.movement.ChangeSpeedMultiplier(1f);
        eaten = false;
        ghost.ChangeColor(ghost.GetInitialColor());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GhostCheckpoint checkpoint = other.GetComponent<GhostCheckpoint>();

        if (checkpoint != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;


            foreach (Vector2 possibleDirection in checkpoint.GetPossibleDirections())
            {

                Vector3 newPosition = transform.position + new Vector3(possibleDirection.x, possibleDirection.y);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {

                    direction = possibleDirection;
                    maxDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    if (enabled)
        //    {
        //        Eaten();
        //    }
        //}

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (enabled)
            {
                Eaten();
                OnGhostEat?.Invoke();
            }
        }
    }


}
