using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : GhostModes
{
    private void OnDisable()
    {
        ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        GhostCheckpoint checkpoint = other.GetComponent<GhostCheckpoint>();

       
        if (checkpoint != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            
            foreach (Vector2 possibleDirection in checkpoint.GetPossibleDirections())
            {
               
                Vector3 newPosition = transform.position + new Vector3(possibleDirection.x, possibleDirection.y);
                float distance = (ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = possibleDirection;
                    minDistance = distance;
                }
            }

            ghost.movement.SetDirection(direction);
        }
    }
}
