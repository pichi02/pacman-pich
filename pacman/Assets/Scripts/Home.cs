using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : GhostModes
{
    [SerializeField] private Transform inside;
    [SerializeField] private Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {

        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ExitHome());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ghost.movement.SetDirection(-ghost.movement.GetDirection());

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ghost.movement.SetDirection(-ghost.movement.GetDirection());

        }
    }

    private IEnumerator ExitHome()
    {

        ghost.movement.SetDirection(Vector2.up, true);
        //ghost.movement.ChangeKinematicRBValue(true);
        ghost.movement.enabled = false;

        Vector3 position = transform.position;

        float duration = 0.5f;
        float elapsed = 0f;


        while (elapsed < duration)
        {
            ghost.SetPosition(Vector3.Lerp(position, inside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < duration)
        {
            ghost.SetPosition(Vector3.Lerp(inside.position, outside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }


        ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
        ghost.movement.enabled = true;
    }
    public Transform GetInside()
    {
        return inside;
    }
    public Transform GetOutside()
    {
        return outside;
    }
}
