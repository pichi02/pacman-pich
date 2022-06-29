using UnityEngine;

public class Scatter : GhostModes
{
    private void OnDisable()
    {
        ghost.chase.Enable();
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        GhostCheckpoint checkpoint = other.GetComponent<GhostCheckpoint>();

       
        if (checkpoint != null && enabled)
        {
          
            int index = Random.Range(0, checkpoint.GetPossibleDirections().Count);

            
            if (checkpoint.GetPossibleDirections()[index] == -ghost.movement.GetDirection())
            {
                index++;

               
                if (index >= checkpoint.GetPossibleDirections().Count)
                {
                    index = 0;
                }
            }

            ghost.movement.SetDirection(checkpoint.GetPossibleDirections()[index]);
       
        }
    }

}