using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject otherPortal;

    public delegate void PortalCollision();
    public event PortalCollision OnPortalCollision;
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = new Vector2(Mathf.RoundToInt(otherPortal.transform.position.x), Mathf.RoundToInt(other.gameObject.transform.position.y));
        OnPortalCollision?.Invoke();
    }
}
