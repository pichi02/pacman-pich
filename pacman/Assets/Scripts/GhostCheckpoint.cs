using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCheckpoint : MonoBehaviour
{
    [SerializeField] LayerMask obstacleLayer;
    private List<Vector2> possibleDirections;
    private void Start()
    {
        possibleDirections = new List<Vector2>();
        CheckPossibleDirection(Vector2.up);
        CheckPossibleDirection(Vector2.down);
        CheckPossibleDirection(Vector2.left);
        CheckPossibleDirection(Vector2.right);

    }

    private void CheckPossibleDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.6f, 0f, direction, 1f, obstacleLayer);


        if (hit.collider == null)
        {
            possibleDirections.Add(direction);
        }
    }
    public List<Vector2> GetPossibleDirections()
    {
        return possibleDirections;
    }
}
