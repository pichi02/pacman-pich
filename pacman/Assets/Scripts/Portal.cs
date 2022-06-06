using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject otherPortal;
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = new Vector2(otherPortal.transform.position.x, other.gameObject.transform.position.y);
    }
}
